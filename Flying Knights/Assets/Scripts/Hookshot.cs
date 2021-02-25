using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hookshot : MonoBehaviour
{
    [SerializeField] private float hookSpeed = 450f;     // Initial speed of the hook
    [SerializeField] private float hookAcceleration = 200f;     // Initial speed of the hook
    [SerializeField] private GameObject hookObject = null;         // Extremity of the hook
    [SerializeField] private ReticleChanger reticle = null; // Reference to the reticle
    [SerializeField] public float maxDist = 2500;     // Maximal distance of the hook
    [SerializeField] private GameObject hookSpawn = null;
    [SerializeField] private float tractionForce = 15000f;

    private LineRenderer lineRend = null;
    private bool isActive;     // True while the player hold the button
    private bool isHooked;     // True if the hook touch an obstacle
    private Collider target = null;   // Collider of the hooked object
    private CustomJoint joint = null;
    private Rigidbody hookRigidBody = null;
    private Vector3 dir = Vector3.zero;
    
    private Vector3 originalHookScale;
    private RaycastHit predictedHit;

    // Start is called before the first frame update
    void Start()
    {
        joint = GetComponent<CustomJoint>();
        joint.SetActive(false);
        hookObject.transform.position = hookSpawn.transform.position;
        hookObject.SetActive(false);
        lineRend = GetComponent<LineRenderer>();
        lineRend.enabled = false;
        hookRigidBody = hookObject.GetComponent<Rigidbody>();
        originalHookScale = hookObject.transform.lossyScale;
    }

    private void setHookGlobalScale()
    {
        hookObject.transform.localScale = Vector3.one;
        hookObject.transform.localScale = new Vector3(originalHookScale.x/hookObject.transform.lossyScale.x,originalHookScale.y/hookObject.transform.lossyScale.y,originalHookScale.z/hookObject.transform.lossyScale.z);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        // While hook is launched but hasn't encountered an obstacle
        if (isActive && !isHooked)
        {
            // Continue forward
            MoveForward();
            Physics.Raycast(hookObject.transform.position, dir, out predictedHit, 100);
        }
    }

    void Update()
    {
        // updates the display positions of the line renderer
        lineRend.SetPosition(0,hookSpawn.transform.position);
        lineRend.SetPosition(1,hookObject.transform.position);
    }

    public void LaunchHook()
    {
        hookObject.SetActive(true);
        hookObject.transform.SetParent(null,true);
        setHookGlobalScale();
        hookObject.transform.position = hookSpawn.transform.position;
        hookRigidBody.velocity = Vector3.zero;


        lineRend.enabled = true;

        // Get reticle point here
        Vector3 target = reticle.GetRaycastHit();
        dir = (target - hookSpawn.transform.position).normalized;

        
        // Set the hook active
        isHooked = false;
        isActive = true;
    }

    public void Hooked(Collision col)
    {
        isHooked = true;
        target = col.collider;
        hookObject.transform.SetParent(target.transform,true);
        setHookGlobalScale();
        Physics.IgnoreCollision(hookObject.GetComponent<Collider>(), target);
        //this allows us to have perfect accuraty for the hooking point using hitscan technique
        if(target==predictedHit.collider)
        {
            hookObject.transform.position = predictedHit.point;
        }
        hookRigidBody.velocity = Vector3.zero;

        //traction force/rope
        joint.SetActive(true,(transform.position - hookObject.transform.position).magnitude + 1);
    }

    public void StopHook()
    {
        if (!isActive)
            return;
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
    }

    // Once the hook is launched, this method is called each frame to move it
    private void MoveForward()
    {
        hookRigidBody.MovePosition(hookObject.transform.position + dir*hookAcceleration*Time.deltaTime);
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
