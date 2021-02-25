using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Tooltip("The jump force, in gravity force (so that changing g won't change jump heigth")]
    [SerializeField] private float jumpForce = 10000f;

    [Tooltip("The horizontal rotation speed of the model")]
    [SerializeField] private float rotationSpeed = 3f;

    [Tooltip("The player lateral acceleration on the ground")]
    [SerializeField] private float playerGroundAcceleration = 40000f;

    [Tooltip("Not a hard limit, if the player exceeds this speed in the direction he wants to accelerate he won't be able to")]
    [SerializeField] private float playerMaxGroundSpeed = 10f;

    [Tooltip("The player lateral acceleration in the air without pressing space")]
    [SerializeField] private float playerAirAccelerationBase = 4000f;

    [Tooltip("The player lateral acceleration in the air when pressing space")]
    [SerializeField] private float playerAirAccelerationAssisted = 8000f;

    [Tooltip("The player vertical acceleration in the air when pressing space, in gravity force")]
    [SerializeField] private float playerAirAssistVerticalForce = 3f;

    [Tooltip("The force when evading the player")]
    [SerializeField] private float playerEvadeForce = 200000f;

    [Tooltip("The time for which we are lock during evasion")]
    [SerializeField] private float evasionTime = 1f;

    [Tooltip("The reticle script")]
    [SerializeField] private GrapplingCooldown GrapplingHookCooldownScript;

    [Tooltip("The camera, it must hold a cameraControler script")]
    [SerializeField] private CameraControler cam;

    [Tooltip("The particle system that makes the trail")]
    [SerializeField] private GameObject trailParticleEmitter;

    [Tooltip("The particle system that makes the impact cloud")]
    [SerializeField] private ParticleSystem impactCloudParticleEmitter;

    [SerializeField] private Pause pause;
    
    float rightMoveValue, forwardMoveValue; //this is updated via the input 
    Vector3 desiredHeading; //this represents the direction where the character should be heading
    bool spaceTrigger = false; 
    bool spaceDown = false; 
    bool onGround = true; //are we currently on the ground
    private Rigidbody localRigidBody;
    private Hookshot hookShootScript;
    private bool airAssisting = false; //air assist by pressing space when in the air
    private bool evadingTrigger = false; //evading can be done on the ground only, this is the trigger
    private float evadingTimeBegin ;
    private bool evading = false; //this means we are in the evading animation (locked)
    private LayerMask raycastLayerToExclude;
    private RaycastHit groundHit;

    void Start()
    {
        localRigidBody = GetComponent<Rigidbody>();
        hookShootScript = GetComponent<Hookshot>();
        raycastLayerToExclude = LayerMask.GetMask("Player");
        trailParticleEmitter.SetActive(false);
    }

    public void FixedUpdate()
    {
        //are we in the air or on the ground ?
        if(Physics.Raycast(transform.position, -transform.up, out groundHit, 1.4f,~raycastLayerToExclude))
        {
            Debug.DrawLine(transform.position,transform.position-transform.up*1.4f,Color.cyan,0.2f);
            if(localRigidBody.velocity.y*localRigidBody.velocity.y <= 0.0001 && !hookShootScript.GetisHooked() )
            {
                if(onGround == false) impactCloudParticleEmitter.Play();
                onGround = true;
                airAssisting = false;
                trailParticleEmitter.SetActive(false);
            }
            else
            {
                onGround = false;
            }
        }
        else
        {
            onGround = false;
        }
        
        //space management
        if(spaceTrigger)
        {
            spaceTrigger = false;
            if(onGround&&spaceDown)
            {
                trailParticleEmitter.SetActive(true);
                airAssisting = true;
                onGround = false;
                localRigidBody.AddForce(Vector3.up * jumpForce * Physics.gravity.magnitude);
            }
            else
            {
                trailParticleEmitter.SetActive(!airAssisting);
                airAssisting = !airAssisting;
            }
            
        }
        
        

        //evading ?
        if(evadingTrigger)
        {
            localRigidBody.AddForce(desiredHeading * playerEvadeForce);
            evadingTrigger = false;
            evading = true;
            evadingTimeBegin = Time.time;
        }

        if(evading)
        {
            if(Time.time - evadingTimeBegin > evasionTime)
            {
                evading = false;
            }
        }
        else
        {
            //set the desired heading
            if(rightMoveValue*rightMoveValue >= 0.01 || forwardMoveValue*forwardMoveValue >= 0.01)
            {
                desiredHeading = ((Vector3.ProjectOnPlane(cam.transform.right,Vector3.up).normalized  * rightMoveValue) + Vector3.ProjectOnPlane(cam.transform.forward,Vector3.up).normalized * forwardMoveValue).normalized ;
                //adding the force depending on the current velocity and state
                if(onGround)
                {
                    if(Vector3.Dot(desiredHeading,localRigidBody.velocity) < playerMaxGroundSpeed)
                    {
                        localRigidBody.AddForce(desiredHeading * playerGroundAcceleration);
                    }
                }
                else
                {
                    if(airAssisting)
                    {
                        localRigidBody.AddForce(desiredHeading * playerAirAccelerationAssisted);
                        localRigidBody.AddForce(Vector3.up * playerAirAssistVerticalForce * Physics.gravity.magnitude);
                    }
                    else
                    {
                        localRigidBody.AddForce(desiredHeading * playerAirAccelerationBase);
                    }
                }
            }
        }
        
        
        

        //rotating toward heading
        transform.rotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(Vector3.RotateTowards(transform.forward,desiredHeading,rotationSpeed*Time.deltaTime,0),Vector3.up).normalized,Vector3.up);

    }

    public void OnMoveRight(InputValue val)
    {
        rightMoveValue = val.Get<float>();
    }
    public void OnMoveForward(InputValue val)
    {
        forwardMoveValue = val.Get<float>();
    }

    public void OnSpaceToggle()
    {
        spaceTrigger = !spaceTrigger;
        spaceDown = !spaceDown;
    }

    public void OnEvade()
    {
        if(!evading)evadingTrigger = true;
    }

    public void OnShotGrappling()
    {
        GrapplingHookCooldownScript.isReady = false;
        hookShootScript.LaunchHook();
    }

    public void OnStopGrappling()
    {
        hookShootScript.StopHook();
    }

    public void OnPause()
    {
        //Time.timeScale = 0f;
        if (pause.Paused)
        {
            pause.Resume();
        }
        else
        {
            pause.ActivatePause();
        }
    }
    
}
