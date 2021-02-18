using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hookshot : MonoBehaviour
{
    [SerializeField] private float hookSpeed = 10f;     // Initial speed of the hook
    [SerializeField] private float hookAcceleration = 100f;     // Initial speed of the hook
    [SerializeField] private GameObject hookObject = null;         // Extremity of the hook
    [SerializeField] private ReticleChanger reticle = null; // Reference to the reticle
    [SerializeField] public float maxDist = 60;     // Maximal distance of the hook
    [SerializeField] private GameObject hookSpawn = null;
    [SerializeField] private float tractionForce = 5000f;

    private LineRenderer lineRend = null;
    private bool isActive;     // True while the player hold the button
    private bool isHooked;     // True if the hook touch an obstacle
    private Collider target = null;   // Collider of the hooked object
    private customJoint joint = null;
    private Rigidbody hookRigidBody = null;
    private Vector3 dir = Vector3.zero;
    

    // Start is called before the first frame update
    void Start()
    {
        joint = GetComponent<customJoint>();
        joint.SetActive(false);
        hookObject.transform.position = hookSpawn.transform.position;
        hookObject.SetActive(false);
        lineRend = GetComponent<LineRenderer>();
        lineRend.enabled = false;
        hookRigidBody = hookObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // updates the display positions of the line renderer
        lineRend.SetPosition(0,hookSpawn.transform.position);
        lineRend.SetPosition(1,hookObject.transform.position);
        // While hook is launched but hasn't encountered an obstacle
        if (isActive && !isHooked)
        {
            // Continue forward
            MoveForward();
        }
    }

    public void LaunchHook()
    {
        hookObject.SetActive(true);
        hookObject.transform.parent = null;
        hookObject.transform.position = hookSpawn.transform.position;
        hookRigidBody.velocity = Vector3.zero;

        lineRend.enabled = true;

        // Get reticle point here
        Vector3 target = reticle.GetRaycastHit();
        Debug.Log("ha");
        Debug.Log(target);
        dir = (target - hookSpawn.transform.position).normalized;
        
        // Set the hook active
        isHooked = false;
        isActive = true;
    }

    public void Hooked(Collision col)
    {
        isHooked = true;
        target = col.collider;
        hookObject.transform.parent = target.transform;
        Physics.IgnoreCollision(hookObject.GetComponent<Collider>(), target);
        hookObject.transform.position = col.GetContact(0).point;
        hookRigidBody.velocity = Vector3.zero;

        //traction force
        //GetComponent<Rigidbody>().AddForce((hookObject.transform.position-transform.position)*Time.deltaTime*tractionForce);
        joint.SetActive(true);
        joint.maxDistance = (transform.position - hookObject.transform.position).magnitude + 1;
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

        hookObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        hookObject.transform.parent = transform;
        hookObject.transform.position = hookSpawn.transform.position;
        hookObject.SetActive(false);
        joint.SetActive(false);
        transform.parent = null;
        lineRend.enabled = false;
    }

    // Once the hook is launched, this method is called each frame to move it
    private void MoveForward()
    {
        if(hookRigidBody.velocity.magnitude < hookSpeed )
        {
            hookRigidBody.AddForce(dir* hookAcceleration);
        }
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
