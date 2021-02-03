using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class CameraControler : MonoBehaviour
{
    [SerializeField] private GameObject cameraPivot = null;
    [SerializeField] private float xSensitivity = 60;
    [SerializeField] private float ySensitivity = 60;
    [SerializeField] private float yLimit = 5;
    [SerializeField] private float defaultZoomLevel = 5;

    private float xAngle = 0;
    private float yAngle = 0;
    private Vector3 lookDirection = new Vector3(1,0,0);
    private Vector3 lateralDirection = new Vector3(0,1,0);
    private float currentZoomLevel = 3;
    private RaycastHit cameraObstructionChecker;


    // Start is called before the first frame update
    void Start()
    {
        //locking the cursor inside the screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //capturing mouse inputs
        xAngle += (Mouse.current.delta.x.ReadValue() * xSensitivity * Time.deltaTime) % 180;
        yAngle -= (Mouse.current.delta.y.ReadValue() * ySensitivity   * Time.deltaTime) % 180;
        //limiting vertical range to avoid inversion
        if(yAngle > 90-yLimit) yAngle = 90-yLimit;
        else if(yAngle < (-90)+yLimit) yAngle = (-90)+yLimit;

        //finding the direction where to look at from the angles we have
        lookDirection = Quaternion.AngleAxis(xAngle,Vector3.up) * Vector3.forward;
        lateralDirection = Vector3.Cross(Vector3.up,lookDirection).normalized;
        lookDirection = Quaternion.AngleAxis(yAngle,lateralDirection) * lookDirection;

        //managing camera obstruction by making the camera closer to the player
        if(Physics.Raycast(cameraPivot.transform.position,-lookDirection,out cameraObstructionChecker,defaultZoomLevel+0.1f))
        {
            currentZoomLevel = cameraObstructionChecker.distance - 0.3f;
        }
        else
        {
            currentZoomLevel = defaultZoomLevel;
        }

        //positioning and orienting camera according to look direction
        transform.position = cameraPivot.transform.position - currentZoomLevel*lookDirection;
        transform.LookAt(cameraPivot.transform.position + lookDirection,Vector3.up);
    }

    public Vector3 GetLateral()
    {
        return lateralDirection;
    }
    public Vector3 GetVertical()
    {
        return lookDirection;
    }
}
