using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBullet : MonoBehaviour
{
    [Header("Bullet Stats")]
    public float bulletSpeed = 20f;
    private Rigidbody2D rb;
    private float maxAge = 3f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = transform.up * bulletSpeed;
        }
        Destroy(gameObject, maxAge);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
    }
}
