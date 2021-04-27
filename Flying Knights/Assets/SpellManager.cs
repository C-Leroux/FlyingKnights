using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] CapsuleCollider c;
    [SerializeField] GameObject Impact;
    [SerializeField] GameObject Miss;
    [SerializeField] double maxTime = 0.7;
    private float Timer = 0;
    [SerializeField] int speed = 2;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = speed * (rb.velocity.normalized);
        Timer += Time.deltaTime;
        if (Timer >= maxTime)
        {
            //Instantiate(Miss, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!(other.gameObject.tag=="Player"|| other.gameObject.tag == "Detection" ))
        {
            Explosion();
           
        }
       
    }

    private void Explosion()
    {
        Instantiate(Impact, gameObject.transform.position, gameObject.transform.rotation);
        Destroy(gameObject);
    }
}
