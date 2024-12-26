using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthComponent : HealthComponent
{
    public HealthBar healthBar;

    public override void Start()
    {
        if (healthBar == null)
        {
            GameObject healthBarObject = GameObject.Find("HealthBar");
            if (healthBarObject != null)
            {
                healthBar = healthBarObject.GetComponent<HealthBar>();
            }
        }

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public override void Subtract(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
            GameManager.Instance.LevelManager.LoadScene("Dead");
        }
        healthBar.SetHealth(currentHealth);
    }

    public override void UpdateHealth()
    {
        healthBar.SetHealth(currentHealth);
    }
}