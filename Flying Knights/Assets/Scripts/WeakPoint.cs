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
            this.gameObject.GetComponent<AudioSource>().Play();
        }
    }
}
