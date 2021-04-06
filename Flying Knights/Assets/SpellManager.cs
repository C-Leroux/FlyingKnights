using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] CapsuleCollider c;
    [SerializeField] GameObject Impact;
    [SerializeField] double maxTime = 0.7;
    private float Timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
        if (Timer >= maxTime)
        {
            Explosion();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!(other.gameObject.tag=="Player"|| other.gameObject.tag == "Detection" ))
        {
            Explosion();
            //Debug.Log(other.name);
           
        }
       
    }

    private void Explosion()
    {
        Instantiate(Impact, gameObject.transform.position, gameObject.transform.rotation);
        Destroy(gameObject);
    }
}
