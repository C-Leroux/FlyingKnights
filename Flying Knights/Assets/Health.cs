using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public GameObject DeathScreen;
    public Text endText;
    public HealthBar healthBar;
    public Score score;

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
        currentHealth -= damage;
    }
}

