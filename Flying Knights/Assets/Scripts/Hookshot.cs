using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hookshot : MonoBehaviour
{
    [SerializeField] private float hookSpeed = 1f;     // Initial speed of the hook
    [SerializeField] private GameObject hookObject = null;         // Extremity of the hook
    [SerializeField] private ReticleChanger reticle = null; // Reference to the reticle
    [SerializeField] public float maxDist = 60;     // Maximal distance of the hook
    [SerializeField] private GameObject hookSpawn = null;
    [SerializeField] private GameObject playerJointObject = null;
    [SerializeField] private PlayerJoint jointScript = null;
    [SerializeField] private float tractionForce = 5000f;
    private LineRenderer lineRend = null;
    private CharacterJoint playerJoint = null; //the joint between the rope and the player
    private bool isActive;     // True while the player hold the button
    private bool isHooked;     // True if the hook touch an obstacle


    private Vector3 dir;       // Direction in which the hook travel
    private Collider target;   // Collider of the hooked object
    private float step;        // Current speed of the hook, decrease with time of travel
    

    // Start is called before the first frame update
    void Start()
    {
        step = hookSpeed;
        hookObject.transform.position = hookSpawn.transform.position;
        hookObject.SetActive(false);
        lineRend = GetComponent<LineRenderer>();
        lineRend.enabled = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        lineRend.SetPosition(0,hookSpawn.transform.position);
        lineRend.SetPosition(1,hookObject.transform.position);
        // While hook is launched but hasn't encountered an obstacle
        if (isActive && !isHooked)
        {
            // Continue forward
            MoveForward();
        }

        // While the player hold the button and is hooked to something
        //if (isActive && isHooked)
        //{
            // Traction toward the hook point
        //    Traction();
        //}
    }

    public void LaunchHook()
    {
        hookObject.SetActive(true);
        hookObject.transform.parent = null;

        lineRend.enabled = true;

        // Get reticle point here
        Vector3 target = reticle.GetRaycastHit();
        dir = (target - hookSpawn.transform.position).normalized;

        isHooked = false;
        isActive = true;
    }

    public void Hooked(Collider targetCollider)
    {
        isHooked = true;
        target = targetCollider;
        hookObject.transform.parent = target.transform;
        Physics.IgnoreCollision(hookObject.GetComponent<Collider>(), target);
        hookObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;

        //traction force
        GetComponent<Rigidbody>().AddForce((hookObject.transform.position-transform.position)*Time.deltaTime*tractionForce);

        //we sort out the joints
        playerJointObject.transform.parent = null;
        playerJointObject.transform.position = transform.position;
        transform.parent = playerJointObject.transform;

        //first the joint betweeen the rope and the player
        playerJoint = gameObject.AddComponent<CharacterJoint>() as CharacterJoint;
        SoftJointLimit limits = new SoftJointLimit();
        limits.limit = 177;
        SoftJointLimitSpring springLimits = new SoftJointLimitSpring();
        springLimits.spring = 1;
        springLimits.damper = 1;
        SoftJointLimit limits2 = new SoftJointLimit();
        limits2.limit = 0;
        playerJoint.twistLimitSpring = springLimits;
        playerJoint.highTwistLimit = limits;
        playerJoint.swing1Limit = limits;
        playerJoint.swing2Limit = limits;
        playerJoint.lowTwistLimit = limits2;
        playerJoint.connectedBody = playerJointObject.GetComponent<Rigidbody>();
        
        //second the joint between the rope and the hook
        jointScript.hook(hookObject.GetComponent<Rigidbody>(),(hookObject.transform.position - hookSpawn.transform.position).magnitude,hookSpawn);

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

        hookObject.transform.parent = transform;
        hookObject.transform.position = hookSpawn.transform.position;
        hookObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        step = hookSpeed;
        jointScript.unHook();
        hookObject.SetActive(false);
        Destroy(playerJoint);
        transform.parent = null;
        playerJointObject.transform.parent = transform;
        lineRend.enabled = false;
    }

    // Once the hook is launched, this method is called each frame to move it
    private void MoveForward()
    {
        hookObject.transform.localPosition += dir * step;

        // If max distance is reached, the hook stop
        if (Vector3.Distance(hookObject.transform.position, transform.position) >= maxDist)
        {
            StopHook();
        }
    }

    // Once something is hooked, the player is pulled toward the collision point
    private void Traction()
    {
        //transform.position += dir * step / 2;
    }

    public bool GetisHooked()
    {
        return isHooked;
    }
}
