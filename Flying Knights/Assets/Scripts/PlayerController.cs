using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public GrapplingCooldown GPCD;
    public Hookshot hs;
    public Rigidbody rb;
    float AxisX, AxisY;
    public CameraControler cam;

    public float jumpForce;
    bool isJumping = false;
    bool onGround = true;
    // Start is called before the first frame update
    void Start()
    {
        rb.GetComponent<Rigidbody>();
    }

    public void FixedUpdate()
    {
        if (isJumping)
        {
            isJumping = false;
            rb.AddForce(Vector3.up * jumpForce);
        }
        //rb.transform.Translate(cam.GetLateral() * AxisX * Time.deltaTime);
        //rb.transform.Translate(cam.GetVertical() * AxisY * Time.deltaTime);
        //rb.MovePosition(transform.position+cam.GetLateral() * 10f * AxisX * Time.deltaTime + cam.GetVertical() * AxisY *10f* Time.deltaTime);
        rb.AddForce(cam.GetLateral() * 1000f * AxisX * Time.deltaTime + cam.GetVertical() * AxisY *1000f* Time.deltaTime);
        //rb.MovePosition(transform.position+cam.GetVertical() * AxisY * Time.deltaTime);
        if(rb.velocity.y == 0)
        {
            onGround = true;
        }
    }

    public void OnMoveHorizontal(InputValue val)
    {
        AxisX = val.Get<float>();
    }
    public void OnMoveVertical(InputValue val)
    {
        AxisY = val.Get<float>();
    }

    public void OnJump()
    {
        if (onGround)
        {
            isJumping = true;
            onGround = false;
        }
    }

    public void OnDash()
    {
        Vector3 dir = cam.GetLateral() * AxisX + cam.GetVertical() * AxisY;
        rb.AddForce(dir * 300f);
    }

    public void OnShotGrappling()
    {
        GPCD.isReady = false;
        hs.LaunchHook();
    }

    public void OnStopGrappling()
    {
        hs.StopHook();
    }
}
