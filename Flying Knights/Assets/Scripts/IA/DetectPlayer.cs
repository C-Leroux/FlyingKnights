using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    private bool isPlayerIn = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isPlayerIn = true;
<<<<<<< HEAD
            // add function in playercontroller script to activate the detection feedback
        }
=======
       
>>>>>>> e127a0d0e81965a92392fe772971ac26ff21966f
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isPlayerIn = false;
            // add function in playercontroller script to desactivate the detection feedback
        }
    }

    public bool IsPlayerDetected()
    {
        return isPlayerIn;
    }
}
