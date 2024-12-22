using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class HitboxComponent : MonoBehaviour
{
    public HealthComponent health;

    void Start()
    {
        health = GetComponent<HealthComponent>();
    }

    public void Damage(int damage)
    {
        health.Subtract(damage);
    }

    public void Damage(GunBullet bullet)
    {
        AttackComponent attackComponent = bullet.GetComponent<AttackComponent>();
        if (attackComponent != null)
        {
            health.Subtract(attackComponent.damage);
        }
    }
}
