using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PAttackBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "WeakPoint")
        {
            Debug.Log("I hit the Colossus weak point");
            // Call a function in the colossus script to activate the damage.
        }
    }
}
