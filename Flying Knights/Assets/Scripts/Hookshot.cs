using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hookshot : MonoBehaviour
{
    [SerializeField] private float hookSpeed = 2000f;     // Initial speed of the hook
    [SerializeField] private float hookAcceleration = 1500f;     // Initial speed of the hook
    [SerializeField] private GameObject hookObject = null;         // Extremity of the hook
    [SerializeField] private ReticleChanger reticle = null; // Reference to the reticle
    [SerializeField] public float maxDist = 250;     // Maximal distance of the hook
    [SerializeField] private GameObject hookSpawn = null;
    [SerializeField] private LayerMask IgnoreInRaycast;

    private LineRenderer lineRend = null;
    private bool isActive;     // True while the player hold the button
    private bool isHooked;     // True if the hook touch an obstacle
    private Collider target = null;   // Collider of the hooked object
    private CustomJoint joint = null;
    private Rigidbody hookRigidBody = null;
    private Vector3 dir = Vector3.zero;
    private Vector3 originalHookScale;
    private RaycastHit collisionDetector;
    private Vector3 previousPos;
    private Vector3 currentHookSpeed = Vector3.zero;
    private AudioSource hookSource;

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
        currentHookSpeed += dir*hookAcceleration*Time.deltaTime;
        if(currentHookSpeed.magnitude > hookSpeed) currentHookSpeed = hookSpeed*currentHookSpeed.normalized;

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

}
