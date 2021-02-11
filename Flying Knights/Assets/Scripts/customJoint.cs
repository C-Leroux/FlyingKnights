using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class customJoint : MonoBehaviour
{
    [SerializeField] public GameObject jointBase = null;
    [SerializeField] public float maxDistance = 10;
    private Rigidbody mRigidBody = null;
    private float thetaPrime;
    private Vector3 rValue;

    // Start is called before the first frame update
    void Start()
    {
        mRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(jointBase != null)
        {
            rValue = jointBase.transform.position-transform.position;
            if(rValue.magnitude > maxDistance)
            {
                thetaPrime = (Vector3.ProjectOnPlane(mRigidBody.velocity,rValue)).magnitude;
                mRigidBody.AddForce((thetaPrime*thetaPrime*rValue - Vector3.Dot(rValue.normalized,Physics.gravity)*rValue.normalized)*Time.deltaTime*mRigidBody.mass);
            }

        }
        
    }
}
