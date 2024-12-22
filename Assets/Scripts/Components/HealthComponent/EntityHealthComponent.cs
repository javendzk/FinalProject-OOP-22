using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityHealthComponent : HealthComponent
{
    public override void Start()
    {
        currentHealth = maxHealth;
    }

    public float Health
    {
        get { return currentHealth; }
    }

    public override void Subtract(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public override void UpdateHealth(){}
}
