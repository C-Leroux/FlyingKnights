using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == "PlayerAttack")
        {
            Debug.Log("Critical hit !");

        }
    }
}
