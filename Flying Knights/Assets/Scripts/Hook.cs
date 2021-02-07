using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    private Hookshot hookshot;

    // Start is called before the first frame update
    void Start()
    {
        hookshot = FindObjectOfType<Hookshot>();

        GameObject player = FindObjectOfType<PlayerController>().gameObject;
        Physics.IgnoreCollision(GetComponent<Collider>(), player.GetComponent<Collider>());
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Test if the collider hit is valid
        if (collision.collider.name != "Hookshot")
        {
            hookshot.Hooked(collision.collider);
        }
    }
}
