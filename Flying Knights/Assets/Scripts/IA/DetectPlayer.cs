using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    private bool isPlayerIn = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            isPlayerIn = true;
       
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            isPlayerIn = false;
    }

    public bool IsPlayerDetected()
    {
        return isPlayerIn;
    }
}
