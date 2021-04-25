using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomJoint : MonoBehaviour
{
    [Tooltip("The other end of the joint, this is the 'rope' base")]
    [SerializeField] private GameObject jointBase = null;

    [Tooltip("This is the traction force that will continuously be applied in the direction of the joint base")]
    [SerializeField] private float TractionForce = 8000f; 
    
    [Tooltip("This drag will be used when the joint is active to provide some damping")]
    [SerializeField] private float airDrag = 0.3f; 
    private float maxDistance; //the distance where the hard limit lies from the joint base, se
    private Rigidbody mRigidBody = null; //rigidbody to use for physic
    private Vector3 rValue; //direction toward the base
    private bool active = false; // whether the joint is active
    private float defaultDrag; //the drag that will be restored when the joint is off
    private PlayerController mHookShoot = null;
    private Vector3 forceToApply = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        mRigidBody = GetComponent<Rigidbody>();
        defaultDrag = mRigidBody.drag;
        mHookShoot = GetComponent<PlayerController>();
    }

    //setter for activating the joint, distance is the hard limit distance
    public void SetActive(bool activeValue,float distance)
    {
        active = activeValue;
        maxDistance = distance;
        if(active)
        {
            mRigidBody.drag = airDrag;
        }
        else
        {
            mRigidBody.drag = defaultDrag;
            mHookShoot.setForceEffect(Vector3.zero);
        }
    }

    //setter for activating the joint
    public void SetActive(bool activeValue)
    {
        SetActive(activeValue,0);
    }

    void FixedUpdate()
    {
        if(active && jointBase != null)
        {
            rValue = jointBase.transform.position-transform.position;
            float  length = rValue.magnitude;
            //mRigidBody.AddForce(rValue.normalized*TractionForce);

            
            //shamelessly changing velocity to make it look like there is a limit
            if(length >= maxDistance)
            {
                forceToApply = rValue.normalized*TractionForce*(1f+((length-maxDistance)/maxDistance));
                mRigidBody.AddForce(forceToApply);
                mHookShoot.setForceEffect(forceToApply);
            }
            else
            {
                forceToApply = rValue.normalized*TractionForce;
                mRigidBody.AddForce(forceToApply);
                mHookShoot.setForceEffect(forceToApply);
                if(length <= 0.9f*maxDistance)
                {
                    maxDistance = 0.9f*maxDistance;
                }
            }
            
        }
        
    }
}
