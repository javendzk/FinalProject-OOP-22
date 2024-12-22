using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class AttackComponent : MonoBehaviour
{
    public GunBullet bullet;
    public int damage;

    void Start()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(gameObject.tag))
        {
            return;
        }

        Transform highestParent = transform;
        while (highestParent.parent != null)
        {
            highestParent = highestParent.parent;
            if ((highestParent.CompareTag(other.tag)) && 
                (highestParent.CompareTag("Player") || highestParent.CompareTag("FishEnemy")))
            {
                return;
            }
        }

        InvincibilityComponent invincibility = other.GetComponent<InvincibilityComponent>();
        if (invincibility != null)
        {
            invincibility.StartInvincibility();

            HitboxComponent hitbox = other.GetComponent<HitboxComponent>();
            if (hitbox != null)
            {
                GunBullet bullet = GetComponent<GunBullet>();
                if (bullet != null)
                {
                    hitbox.Damage(bullet); 
                }
                else
                {
                    hitbox.Damage(damage);
                }
            }
        }
    }

    void Update()
    {
        // ...existing code...
    }
}
