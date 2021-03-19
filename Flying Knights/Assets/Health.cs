using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private float invincibilityLength = 1.5f;
    [SerializeField] private GameObject DeathScreen;
    [SerializeField] private Text endText;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private Score score;
    private int currentHealth;
    private bool invincible = false;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {

        healthBar.SetHealth(currentHealth);
        if (healthBar.getHealth() <= 0)
        {
            DeathScreen.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;
            endText.text = "Game Over ! \n votre score est de " + score.scoreValue;
        }
       
    }

    void TakeDamage(int damage)
    {
        if(!invincible)
        {
            currentHealth -= damage;
            invincible = true;
            Invoke("DisableInvincibility",invincibilityLength);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "ColossusAttack")
        {
            TakeDamage(maxHealth/2);
        }
    }

    void DisableInvincibility()
    {
        invincible = false;
    }
}

