using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellImpact : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] Colossus colossus;
    // Start is called before the first frame update
    void Start()
    {
        colossus = GetComponentInParent<Colossus>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Spell"&&this.gameObject.tag!="Detection")
        {
            colossus.Freeze();
           Debug.Log(this.gameObject.tag);
            Debug.Log(this.gameObject.name);
        }
            
        
       
    }
}
