using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hookshot : MonoBehaviour
{
    [SerializeField] private float hookAcceleration = 150f;     // Acceleration of the hook
    [SerializeField] private GameObject hookObject = null;         // Extremity of the hook
    [SerializeField] private ReticleChanger reticle = null; // Reference to the reticle
    [SerializeField] public float maxDist = 250;     // Maximal distance of the hook
    [SerializeField] private GameObject hookSpawn = null;
    [SerializeField] private LayerMask IgnoreInRaycast;
    [SerializeField] private GameObject rightHand = null;
    [SerializeField] private GameObject leftHand = null;


    private LineRenderer lineRend = null;
    private bool isActive;     // True while the player hold the button
    private bool isHooked;     // True if the hook touch an obstacle
    private Collider target = null;   // Collider of the hooked object
    private CustomJoint joint = null;
    private Rigidbody hookRigidBody = null;
    private Rigidbody selfRigidBody = null;
    private Vector3 dir = Vector3.zero;
    private Vector3 originalHookScale;
    private RaycastHit collisionDetector;
    private Vector3 previousPos;
    private Vector3 currentHookSpeed = Vector3.zero;
    private AudioSource hookSource;
    private Vector3 actualHookAcceleration;
    private bool rightHandActive = true;

    // Start is called before the first frame update
    void Start()
    {
        joint = GetComponent<CustomJoint>();
        hookObject.transform.position = hookSpawn.transform.position;
        previousPos = hookObject.transform.position;
        hookObject.SetActive(false);
        lineRend = GetComponent<LineRenderer>();
        lineRend.enabled = false;
        hookRigidBody = hookObject.GetComponent<Rigidbody>();
        selfRigidBody = GetComponent<Rigidbody>();
        originalHookScale = hookObject.transform.lossyScale;
        hookSource = hookObject.GetComponent<AudioSource>();
    }

    //This fixes the scale change when the hook object changes parent
    private void setHookGlobalScale()
    {
        hookObject.transform.localScale = Vector3.one;
        hookObject.transform.localScale = new Vector3(originalHookScale.x/hookObject.transform.lossyScale.x,originalHookScale.y/hookObject.transform.lossyScale.y,originalHookScale.z/hookObject.transform.lossyScale.z);
    }

    void Update()
    {
        

        // Moving the hook object
        // While hook is launched but hasn't encountered an obstacle
        if (isActive && !isHooked)
        {
            // Continue forward
            MoveForward();

            //we check for collision on the last frame
            if(Physics.Raycast(previousPos, dir, out collisionDetector,(hookObject.transform.position-previousPos).magnitude,~IgnoreInRaycast))
            {
                Hooked(collisionDetector.collider);
            }
            
        }
        if(isActive)
        {
            //setting the hand holding the hook
            if(Vector3.Cross( Vector3.ProjectOnPlane(transform.forward,Vector3.up) , Vector3.ProjectOnPlane(hookObject.transform.position - transform.position,Vector3.up) ).y > 0)
            {
                hookSpawn.transform.SetParent(rightHand.transform);
                hookSpawn.transform.position = rightHand.transform.position;
                rightHandActive = true;
            }
            else
            {
                hookSpawn.transform.SetParent(leftHand.transform);
                hookSpawn.transform.position = leftHand.transform.position;
                rightHandActive = false;
            }
        }
        
        // Updates the display positions of the line renderer
        lineRend.SetPosition(0,hookSpawn.transform.position);
        lineRend.SetPosition(1,hookObject.transform.position);

        // This is needed to manually find collisions
        previousPos = hookObject.transform.position;
    }

    public void LaunchHook()
    {
        // Setting up the hookObject
        hookObject.SetActive(true);
        hookObject.transform.SetParent(null,true);
        setHookGlobalScale();
        hookObject.transform.position = hookSpawn.transform.position;
        previousPos = hookObject.transform.position;
        hookRigidBody.velocity = Vector3.zero;

        // Setting up the renderer for the line
        lineRend.enabled = true;

        // Get reticle point here
        Vector3 targetVect = reticle.GetRaycastHit();
        dir = (targetVect - hookSpawn.transform.position).normalized;

        //getting the basis we need
        Vector3 normalDirPlane = (Vector3.Cross(dir,selfRigidBody.velocity)).normalized;
        Vector3 perpToDir = (Vector3.Cross(normalDirPlane,dir)).normalized;
        //dir and perpToDir are now an orthogonal basis of the plane defined by our velocity and our target point, with dir being the direction to our target
        
        //the time to the impact assuming the hook moves in a straight line at constant acceleration
        float dirVelocity = Vector3.Dot(selfRigidBody.velocity,dir);
        float dirInitialPosition = Vector3.Dot(hookSpawn.transform.position,dir);
        float dirTargetPosition = Vector3.Dot(targetVect,dir);
        float impactTime = (-dirVelocity + Mathf.Sqrt((dirVelocity*dirVelocity) - (2 * hookAcceleration*(dirInitialPosition - dirTargetPosition ))))/hookAcceleration;

        //now we calculate the perpendicular acceleration required to return to the initial perpendicular position at impact time
        float perpVelocity = Vector3.Dot(selfRigidBody.velocity,perpToDir);
        float lambdaAcceleration = - 2 * perpVelocity / impactTime;

        actualHookAcceleration = hookAcceleration * dir + lambdaAcceleration * perpToDir;
        currentHookSpeed = selfRigidBody.velocity;

        // Set the hook active
        isHooked = false;
        isActive = true;
    }

    public void Hooked(Collider col)
    {
        isHooked = true;
        target = col;
        hookObject.transform.SetParent(target.transform,true);
        setHookGlobalScale();
        Physics.IgnoreCollision(hookObject.GetComponent<Collider>(), target);
        hookObject.transform.position = collisionDetector.point;
        hookRigidBody.velocity = Vector3.zero;
        hookSource.Play();
        //traction force/rope
        joint.SetActive(true,(transform.position - hookObject.transform.position).magnitude + 1);
    }

    public void StopHook()
    {
        if (!isActive) return;
        if (isHooked)
        {
            Physics.IgnoreCollision(hookObject.GetComponent<Collider>(), target, false);
            target = null;
        }
        isActive = false;
        isHooked = false;

        hookRigidBody.constraints = RigidbodyConstraints.FreezeRotation;
        hookObject.transform.SetParent(transform,true);
        setHookGlobalScale();
        hookObject.transform.position = hookSpawn.transform.position;
        hookObject.SetActive(false);
        joint.SetActive(false);

        lineRend.enabled = false;
        currentHookSpeed = Vector3.zero;
    }

    // Once the hook is launched, this method is called each frame to move it
    private void MoveForward()
    {
        currentHookSpeed += actualHookAcceleration*Time.deltaTime;

        hookObject.transform.position += currentHookSpeed*Time.deltaTime;
        // If max distance is reached, the hook stop
        if (Vector3.Distance(hookObject.transform.position, transform.position) >= maxDist)
        {
            StopHook();
        }
    }

    public bool GetisHooked()
    {
        return isHooked;
    }

    public bool GetisActive()
    {
        return isActive;
    }

    public bool getRightHandActive()
    {
        return rightHandActive;
    }

}
