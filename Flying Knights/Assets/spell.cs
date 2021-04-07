using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class spell : MonoBehaviour
{
    [SerializeField] private ReticleChanger reticle = null; // Reference to the reticle
    [SerializeField] GameObject SpellPrefab;
    [SerializeField] private GameObject fireObject = null;
    [SerializeField] float cooldownTime = 15;
    [SerializeField] bool canShoot = true;
    [SerializeField] float timer = 0;
    [SerializeField] Image coolDownImage;

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        coolDownImage.color = new Color32(255, 255, 255, 255);
    }

    // Update is called once per frame
    void Update()
    {
        if (!canShoot)
        {
            timer += Time.deltaTime;
            if (timer >= cooldownTime)
            {
                canShoot = true;
                coolDownImage.color = new Color32(255, 255, 255, 255);
                timer = 0;
            }
        }
        
    }

    public void Shoot()
    {
        if (canShoot)
        {
            Vector3 targetVect = reticle.GetRaycastHit();

            Transform spellT = fireObject.transform;
            GameObject Spell = Instantiate(SpellPrefab, transform.position, transform.rotation);
            rb = (Rigidbody)Spell.GetComponent("Rigidbody");
            rb.AddForce((targetVect - transform.position) * 40f);
            canShoot = false;
            timer = 0;
            coolDownImage.color = new Color32(255, 255, 255, 0);
        }
        

    }
}
