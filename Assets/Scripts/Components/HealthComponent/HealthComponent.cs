using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HealthComponent : MonoBehaviour
{
    [SerializeField] public float maxHealth;
    public float currentHealth;

    public abstract void Start();
    public abstract void Subtract(float amount);
    public abstract void UpdateHealth();
}
