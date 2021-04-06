using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class spell : MonoBehaviour
{
    [SerializeField] private ReticleChanger reticle = null; // Reference to the reticle
    [SerializeField] GameObject SpellPrefab;
    [SerializeField] private GameObject fireObject = null;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot()
    {
        Vector3 targetVect = reticle.GetRaycastHit();

        Transform spellT = fireObject.transform;
        GameObject Spell=Instantiate(SpellPrefab, transform.position, transform.rotation);
        rb = (Rigidbody)Spell.GetComponent("Rigidbody");
        rb.AddForce((targetVect - transform.position)*40f);
    }
}
