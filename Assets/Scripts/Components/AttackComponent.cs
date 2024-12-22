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
        Transform highestParent = GetHighestParent(transform);
        Transform otherHighestParent = GetHighestParent(other.transform);

        if ((highestParent.CompareTag("FishFriendly") && otherHighestParent.CompareTag("FishFriendly")) ||
            (highestParent.CompareTag("FishFriendly") && otherHighestParent.CompareTag("FishEnemy")) ||
            (highestParent.CompareTag("FishEnemy") && otherHighestParent.CompareTag("FishFriendly")) ||
            (highestParent.CompareTag("FishEnemy") && otherHighestParent.CompareTag("FishEnemy")))
        {
            return;
        }

        DealDamage(other);
    }

    private Transform GetHighestParent(Transform obj)
    {
        while (obj.parent != null)
        {
            obj = obj.parent;
        }
        return obj;
    }

    private void DealDamage(Collider2D other)
    {
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
}
