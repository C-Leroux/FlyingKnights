using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJoint : MonoBehaviour
{
    private ConfigurableJoint hookJoint = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void hook(Rigidbody toConnect,float distance,Vector3 localAnchor)
    {
        hookJoint = gameObject.AddComponent<ConfigurableJoint>() as ConfigurableJoint;
        hookJoint.connectedBody = toConnect;
        hookJoint.xMotion = ConfigurableJointMotion.Limited;
        hookJoint.yMotion = ConfigurableJointMotion.Locked;
        hookJoint.zMotion = ConfigurableJointMotion.Locked;
        hookJoint.anchor = transform.InverseTransformPoint(toConnect.transform.position);
        hookJoint.autoConfigureConnectedAnchor = true;
        //hookJoint.connectedAnchor = transform.InverseTransformPoint(localAnchor);
        
        SoftJointLimit limits = new SoftJointLimit();
        limits.limit = distance;
        limits.contactDistance = 5;
        hookJoint.linearLimit = limits;
    }

    public void unHook()
    {
        Destroy(hookJoint);
    }
}
