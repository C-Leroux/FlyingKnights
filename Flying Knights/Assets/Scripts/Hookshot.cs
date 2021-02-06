using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hookshot : MonoBehaviour
{
    private bool isActive;   // True while the player hold the button
    private bool isHooked;   // True if the hook touch an obstacle

    private Camera cam;      // Reference to the main camera

    private Hook hook;       // Extremity of the hook
    private Vector3 dir;     // Direction in which the hook travel

    private float maxStep;   // Initial speed of the hook
    private float step;      // Current speed of the hook, decrease with time of travel

    private float maxDist;   // Maximal distance of the hook

    // Start is called before the first frame update
    void Start()
    {
        cam = FindObjectOfType<Camera>();
        hook = GameObject.FindObjectOfType<Hook>();

        maxStep = 0.1f;
        step = maxStep;

        maxDist = 30;

        LaunchHook();
    }

    // Update is called once per frame
    void Update()
    {
        // While hook is launched but hasn't encountered an obstacle
        if (isActive && !isHooked)
        {
            // Continue forward
            MoveForward();

            // Progressively decrease speed
            // step =
        }

        // While the player hold the button and is hooked to something
        if (isActive && isHooked)
        {
            // Traction toward the hook point
            Traction();
        }
    }

    public void LaunchHook()
    {
        // Get reticle point here
        Vector3 target = new Vector3(0, 10, 10);
        dir = target.normalized;

        isHooked = false;
        isActive = true;
    }

    public void Hooked()
    {
        isHooked = true;
    }

    public void StopHook()
    {
        isActive = false;
        isHooked = false;

        hook.transform.localPosition = new Vector3();
        step = maxStep;
    }

    // Once the hook is launched, this method is called each frame to move it
    private void MoveForward()
    {
        hook.transform.localPosition += dir * step;

        // If max distance is reached, the hook stop
        if (Vector3.Distance(hook.transform.position, transform.position) >= maxDist)
        {
            StopHook();
        }
    }

    // Once something is hooked, the player is pulled toward the collision point
    private void Traction()
    {
        transform.position += dir * step / 2;
    }
}
