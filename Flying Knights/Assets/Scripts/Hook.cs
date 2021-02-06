using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    private Hookshot hookshot;

    // Start is called before the first frame update
    void Start()
    {
        hookshot = GameObject.FindObjectOfType<Hookshot>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Test if the collider hit is valid
        if (collision.collider.name != "Hookshot")
            hookshot.Hooked();
    }
}
