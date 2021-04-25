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

    [Tooltip("The time for which we are locked during evasion")]
    [SerializeField] private float evasionTime = 1f;

    [Tooltip("The reticle script")]
    [SerializeField] private GrapplingCooldown GrapplingHookCooldownScript;

    [Tooltip("The camera, it must hold a cameraControler script")]
    [SerializeField] private CameraControler cam;

    [Tooltip("The particle system that makes the trail")]
    [SerializeField] private GameObject trailParticleEmitter;

    [Tooltip("The particle system that makes the impact cloud")]
    [SerializeField] private ParticleSystem impactCloudParticleEmitter;

    [Tooltip("The animator for the player model")]
    [SerializeField] private Animator playerAnimator;

    [Tooltip("The player model")]
    [SerializeField] private GameObject playerModel;

    [Tooltip("The player tilt speed")]
    [SerializeField] private float playerTiltSpeed;

    [Tooltip("The player maximum tilt angle on each side ")]
    [SerializeField] private float playerMaxTiltAngle = 50;

    [Tooltip("The ratio of force/gravity above which no more tilt is added to the player, tilt scales linearly from zero to this value of ratio")]
    [SerializeField] private float tiltSaturationFactor = 1500f;

    [SerializeField] private Pause pause;
    
    float rightMoveValue, forwardMoveValue; //this is updated via the input 
    Vector3 desiredHeading; //this represents the direction where the character should be heading
    bool spaceTrigger = false; 
    bool spaceDown = false; 
    bool onGround = true; //are we currently on the ground
    private Rigidbody localRigidBody;
    private Hookshot hookShootScript;
    //private bool airAssisting = false; //air assist by pressing space when in the air
    private bool evadingTrigger = false; //evading can be done on the ground only, this is the trigger
    private float evadingTimeBegin ;
    private bool evading = false; //this means we are in the evading animation (locked)
    private LayerMask raycastLayerToExclude;
    private RaycastHit groundHit;
    private float directionFactor = 0f;
    private float velocityFactor = 0f;
    private spell Spell;
    private float targetPlayerTilt = 0; // the current force not due to gravity that is being applied to the player, for tilting effect
    private float currentPlayerTilt =0;

    private AudioSource sfxSource;
    [SerializeField] private AudioSource GrapplingSource;

    void Start()
    {
        localRigidBody = GetComponent<Rigidbody>();
        hookShootScript = GetComponent<Hookshot>();
        raycastLayerToExclude = LayerMask.GetMask("Player");
        trailParticleEmitter.SetActive(false);
        sfxSource = impactCloudParticleEmitter.GetComponent<AudioSource>();
        Cursor.visible = false;
        Spell = GetComponent<spell>();
    }

    private bool checkGround()
    {
        //this checks wether we have hit the ground
        return Physics.Raycast(transform.position+0.3f*transform.forward, -transform.up, out groundHit, 1.3f,~raycastLayerToExclude) ||
        Physics.Raycast(transform.position-0.3f*transform.forward, -transform.up, out groundHit, 1.3f,~raycastLayerToExclude) ||
        Physics.Raycast(transform.position-0.3f*transform.right, -transform.up, out groundHit, 1.3f,~raycastLayerToExclude) ||
        Physics.Raycast(transform.position+0.3f*transform.right, -transform.up, out groundHit, 1.3f,~raycastLayerToExclude);
    }

    public void FixedUpdate()
    {
        //are we in the air or on the ground ?
        if(checkGround())
        {
            if( !hookShootScript.GetisHooked() )
            {
                if (onGround == false)
                {
                    impactCloudParticleEmitter.Play();
                    sfxSource.Play();
                }

                onGround = true;
                //airAssisting = false;
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
                //airAssisting = true;
                onGround = false;
                localRigidBody.AddForce(Vector3.up * jumpForce * Physics.gravity.magnitude);
                playerAnimator.SetTrigger("JumpTrigger");
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
                    if(Vector3.Dot(localRigidBody.velocity,desiredHeading) < playerMaxGroundSpeed +0.05f)
                    {
                        localRigidBody.AddForce(desiredHeading * playerGroundAcceleration);
                    }
                }
                else
                {
                    if(spaceDown)
                    {
                        localRigidBody.AddForce(desiredHeading * playerAirAccelerationAssisted);
                        localRigidBody.AddForce(Vector3.up * playerAirAssistVerticalForce * Physics.gravity.magnitude);
                    }
                    else
                    {
                        localRigidBody.AddForce(desiredHeading * playerAirAccelerationBase);
                    }
                    
                    //here we are hooked, which will override the heading and rotation to get a realistic effect
                    if(hookShootScript.GetisHooked())
                    {
                        desiredHeading = Vector3.ProjectOnPlane(localRigidBody.velocity,Vector3.up).normalized;
                    }
                }

                //setting the forward animation
                if(onGround)
                {
                    velocityFactor = localRigidBody.velocity.magnitude*1.3f;
                    if(velocityFactor > playerMaxGroundSpeed*0.8f) velocityFactor = playerMaxGroundSpeed*0.8f;
                    velocityFactor /= playerMaxGroundSpeed*0.8f;
                }
            }
            else
            {
                //setting the forward animation
                velocityFactor = 0;
                if(!onGround) desiredHeading = Vector3.ProjectOnPlane(localRigidBody.velocity.normalized,Vector3.up);
            }
        }
        

        //rotating toward heading
        if(hookShootScript.GetisHooked())
        {
            transform.rotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(Vector3.RotateTowards(transform.forward,localRigidBody.velocity,rotationSpeed*12*Time.deltaTime,0),Vector3.up).normalized,Vector3.up);
        }
        else
        {
            transform.rotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(Vector3.RotateTowards(transform.forward,desiredHeading,rotationSpeed*Time.deltaTime,0),Vector3.up).normalized,Vector3.up);
        }

        //rotating model to reflect tilt
        currentPlayerTilt = Mathf.MoveTowards(currentPlayerTilt,targetPlayerTilt,playerTiltSpeed);
        playerModel.transform.localRotation = Quaternion.Euler(0,0,currentPlayerTilt);
 
        
        //setting the direction of the animation
        if(onGround)
        {
            //sliding ?
            if(localRigidBody.velocity.magnitude > playerMaxGroundSpeed*1.5f)
            {
                if(localRigidBody.velocity.magnitude > playerMaxGroundSpeed*3.5f)
                {
                    velocityFactor = -1f;
                }
                else
                {
                    velocityFactor = -1f*((localRigidBody.velocity.magnitude-playerMaxGroundSpeed)/playerMaxGroundSpeed)/2f + velocityFactor * (1-(((localRigidBody.velocity.magnitude-playerMaxGroundSpeed)/playerMaxGroundSpeed)/2f) );
                }
            }
            else
            {
                directionFactor = Vector3.SignedAngle(desiredHeading,transform.forward,-Vector3.up)*1.5f;
                if(directionFactor<-90f) directionFactor = -90f;
                if(directionFactor>90f) directionFactor = 90f;
                directionFactor/=90f;
            }
        }
        

    }

    public void Update()
    {
        playerAnimator.SetFloat("Direction",directionFactor);
        playerAnimator.SetFloat("Velocity",velocityFactor);
        playerAnimator.SetBool("OnGround",onGround);

        
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
        trailParticleEmitter.SetActive(spaceDown);
    }

    public void OnEvade()
    {
        if(!evading)evadingTrigger = true;
    }

    public void OnShotGrappling()
    {
        if(GrapplingHookCooldownScript.isReady )
        {
            GrapplingHookCooldownScript.isReady = false;
            hookShootScript.LaunchHook();
            GrapplingSource.Play();
        }
    }

    public void OnStopGrappling()
    {
        hookShootScript.StopHook();
    }

    public void OnPause()
    {
        if (pause.Paused)
        {
            pause.Resume();
        }
        else
        {
            pause.ActivatePause();
        }
    }
    public void OnSpell()
    {
        Spell.Shoot();
    }

    public void setForceEffect(Vector3 Force)
    {
        float forceRatio = Force.magnitude/Physics.gravity.magnitude;
        if(hookShootScript.getRightHandActive())
        {
            if(forceRatio < tiltSaturationFactor)
            {
                targetPlayerTilt = - Vector3.Dot(transform.right,Force.normalized) * playerMaxTiltAngle * forceRatio / tiltSaturationFactor;
            }
            else targetPlayerTilt = - playerMaxTiltAngle;
        }
        else
        {
            if(forceRatio < tiltSaturationFactor)
            {
                targetPlayerTilt = Vector3.Dot(-transform.right,Force.normalized) * playerMaxTiltAngle * forceRatio / tiltSaturationFactor;
            }
            else targetPlayerTilt = playerMaxTiltAngle;
        }
        
    }

}
