using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] CapsuleCollider c;
    [SerializeField] GameObject Impact;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!(other.gameObject.tag=="Player"|| other.gameObject.tag == "Detection" ))
        {
            Instantiate(Impact, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
            //Debug.Log(other.name);
           
        }
       
    }
}
