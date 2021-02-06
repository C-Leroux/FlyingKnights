using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaracterControler : MonoBehaviour
{
    [SerializeField] private GameObject localCamera = null;
    [SerializeField] private float rotationSpeed = 50;
    [SerializeField] private float speed = 3;
    [SerializeField] private float jumpImpulse = 3;
    [SerializeField] private float powerMultiplier = 3;
    private Vector3 heading = new Vector3(1,0,0);
    private Rigidbody localRigidBody;
    private RaycastHit nearGround;

    // Start is called before the first frame update
    void Start()
    {
        heading = this.transform.forward;
        localRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        //capturing inputs and applying force
        //move forward
        bool changed = false;
        bool multiplied = false;
        Vector3 totalDirection = new Vector3(0,0,0);
        if(Input.GetKey(KeyCode.Z))
        {
            totalDirection += (Vector3.ProjectOnPlane(localCamera.transform.forward,Vector3.up)).normalized;
            changed = true;
        }
        //move back
        if(Input.GetKey(KeyCode.S))
        {
            totalDirection += (Vector3.ProjectOnPlane(-localCamera.transform.forward,Vector3.up)).normalized;
            changed = true;
        }
        //left
        if(Input.GetKey(KeyCode.Q))
        {
            totalDirection += (Vector3.ProjectOnPlane(-localCamera.transform.right,Vector3.up)).normalized;
            changed = true;
        }
        //right
        if(Input.GetKey(KeyCode.D))
        {
            totalDirection += (Vector3.ProjectOnPlane(localCamera.transform.right,Vector3.up)).normalized;
            changed = true;
        }

        if(Input.GetKey(KeyCode.Space))
        {
            //on the ground?
            if(Physics.Raycast(transform.position,-Vector3.up,out nearGround,1.2f))
            {
                localRigidBody.AddForce(Vector3.up*jumpImpulse);
            }
            //in the air 
            else
            {
                multiplied = true;
            }
        }

        if(changed)
        {
            heading = totalDirection.normalized;
            localRigidBody.AddForce(heading * speed * Time.deltaTime * (multiplied ? powerMultiplier:1) );
        }

        //rotating toward heading
        transform.rotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(Vector3.RotateTowards(transform.forward,heading,rotationSpeed*Time.deltaTime,0),Vector3.up).normalized,Vector3.up);

        Debug.DrawRay(transform.position,this.transform.forward,Color.blue,0.2f);
        Debug.DrawRay(transform.position,heading,Color.red,0.2f);
    }
}
