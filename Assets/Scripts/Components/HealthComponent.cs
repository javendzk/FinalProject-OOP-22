using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public HealthBar healthBar;
    public float maxHealth = 100f;
    public float currentHealth;

    void Start()
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

    public void Subtract(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
        healthBar.SetHealth(currentHealth);
    }

    public void UpdateHealth()
    {
        healthBar.SetHealth(currentHealth);
    }
}