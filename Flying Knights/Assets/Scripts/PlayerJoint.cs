using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJoint : MonoBehaviour
{
    [SerializeField] float limitDistance = 15;
    private ConfigurableJoint hookJoint = null;

    private GameObject localAnchor = null; 
    bool hooked = false;

    private float calculateDistance()
    {
        return (hookJoint.connectedBody.transform.position - transform.position).magnitude;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    void FixedUpdate()
    {
        
        
    }

    public void hook(Rigidbody toConnect,float distance,GameObject anchor)
    {
        hooked = true;
        hookJoint = gameObject.AddComponent<ConfigurableJoint>() as ConfigurableJoint;
        hookJoint.connectedBody = toConnect;
        hookJoint.xMotion = ConfigurableJointMotion.Locked;
        hookJoint.yMotion = ConfigurableJointMotion.Locked;
        hookJoint.zMotion = ConfigurableJointMotion.Locked;
        localAnchor = anchor;
        //hookJoint.anchor = transform.InverseTransformPoint(localAnchor.transform.position); 
        hookJoint.anchor = transform.InverseTransformPoint(toConnect.transform.position); 
        //hookJoint.autoConfigureConnectedAnchor = true;
        //hookJoint.connectedAnchor = transform.InverseTransformPoint(toConnect.transform.position);
        
        //SoftJointLimit limits = new SoftJointLimit();
        //limits.limit = distance;
        //limits.contactDistance = 4;
        //hookJoint.linearLimit = limits;
    }

    public void unHook()
    {
        hooked = false;
        Destroy(hookJoint);
    }
}
