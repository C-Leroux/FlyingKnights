using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class customJoint : MonoBehaviour
{
    [SerializeField] public GameObject jointBase = null;
    [SerializeField] public float maxDistance = 10;
    [SerializeField] public float TractionForce = 500f; //test
    private Rigidbody mRigidBody = null;
    private float thetaPrime;
    private Vector3 rValue;
    private bool active = false;

    // Start is called before the first frame update
    void Start()
    {
        mRigidBody = GetComponent<Rigidbody>();
    }

    public void SetActive(bool activeValue)
    {
        active = activeValue;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(active && jointBase != null)
        {
            rValue = (jointBase.transform.position-transform.position);
            if(rValue.magnitude >= maxDistance) mRigidBody.AddForce(rValue.normalized*Time.deltaTime*TractionForce);
            
            /*
            rValue = jointBase.transform.position-transform.position;
            if(rValue.magnitude >= maxDistance)
            {
                mRigidBody.velocity = (Vector3.ProjectOnPlane(mRigidBody.velocity,rValue));
                thetaPrime = mRigidBody.velocity.magnitude;
                //mRigidBody.AddForce((thetaPrime*thetaPrime*rValue)*mRigidBody.mass*Time.deltaTime );
                if(transform.position.y < jointBase.transform.position.y)
                {
                    mRigidBody.AddForce(Vector3.Dot(Physics.gravity,-rValue.normalized) * rValue.normalized *mRigidBody.mass *Time.deltaTime);
                }
            }
            */
        }
        
    }
}
