using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakPoint : MonoBehaviour
{
    [SerializeField] Colossus colossus;
    private void Start()
    {
        colossus = GetComponentInParent<Colossus>();
    }
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == "PlayerAttack")
        {
            Debug.Log("Critical hit !");
            colossus.TakeDamage(600);
<<<<<<< HEAD
            this.gameObject.GetComponent<AudioSource>().Play();
=======
    
>>>>>>> 5ccca349ae836875c6dee6aa7eab389941a87c55
        }
    }
}
