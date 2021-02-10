using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJoint : MonoBehaviour
{
    [SerializeField] float limitDistance = 15;
    private ConfigurableJoint hookJoint = null;
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
        if(hooked && calculateDistance() > limitDistance + 0.001 )
        {
            //setting correct position
            transform.position = hookJoint.connectedBody.transform.position + (transform.position - hookJoint.connectedBody.transform.position).normalized*limitDistance;
            //straigthening the speed
            GetComponent<Rigidbody>().velocity = Vector3.ProjectOnPlane(GetComponent<Rigidbody>().velocity,(transform.position - hookJoint.connectedBody.transform.position).normalized) * GetComponent<Rigidbody>().velocity.magnitude;
        }
    }

    public void hook(Rigidbody toConnect,float distance,Vector3 localAnchor)
    {
        hookJoint = gameObject.AddComponent<ConfigurableJoint>() as ConfigurableJoint;
        hookJoint.connectedBody = toConnect;
        hookJoint.xMotion = ConfigurableJointMotion.Limited;
        hookJoint.yMotion = ConfigurableJointMotion.Locked;
        hookJoint.zMotion = ConfigurableJointMotion.Locked;
        hookJoint.anchor = transform.InverseTransformPoint(localAnchor); 
        hookJoint.autoConfigureConnectedAnchor = false;
        hookJoint.connectedAnchor = transform.InverseTransformPoint(toConnect.transform.position);
        
        SoftJointLimit limits = new SoftJointLimit();
        limits.limit = distance;
        limits.contactDistance = 4;
        hookJoint.linearLimit = limits;
    }

    public void unHook()
    {
        Destroy(hookJoint);
    }
}
