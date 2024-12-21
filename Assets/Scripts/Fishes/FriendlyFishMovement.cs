using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyFishMovement : MonoBehaviour
{
    public float slowSpeed = 1f;
    public float fastSpeed = 3f;
    public float fleeSpeed = 5f;
    public float detectionRange = 5f;
    public Transform player;
    private Rigidbody2D rb;
    private Vector2 movement;
    private bool isFleeing = false;
    private BoxCollider2D boxCollider;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        boxCollider = GetComponent<BoxCollider2D>();
        StartCoroutine(RandomMovement());
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer < detectionRange)
        {
            isFleeing = true;
            FleeFromPlayer();
        }
        else
        {
            isFleeing = false;
        }

        FlipSprite();
    }

    IEnumerator RandomMovement()
    {
        while (true)
        {
            if (!isFleeing)
            {
                float speed = Random.Range(slowSpeed, fastSpeed);
                movement = new Vector2(Random.Range(-1f, 1f), Random.Range(-0.5f, 0.5f)).normalized * speed;
                rb.velocity = movement;
            }
            yield return new WaitForSeconds(Random.Range(1f, 3f));
        }
    }

    void FleeFromPlayer()
    {
        Vector2 direction = (transform.position - player.position).normalized;
        rb.velocity = direction * fleeSpeed;
    }

    void FlipSprite()
    {
        Vector3 localScale = transform.localScale;
        if (rb.velocity.x > 0)
        {
            localScale.x = Mathf.Abs(localScale.x);
        }
        else if (rb.velocity.x < 0)
        {
            localScale.x = -Mathf.Abs(localScale.x);
        }
        transform.localScale = localScale;
    }

    public void ShrinkAndMoveTowardsPlayer()
    {
        boxCollider.enabled = false;
        StopCoroutine(RandomMovement());
        StartCoroutine(MoveTowardsPlayerAndShrink());
    }

    IEnumerator MoveTowardsPlayerAndShrink()
    {
        float shrinkDuration = 0.5f;
        float elapsedTime = 0f;
        Vector3 initialScale = transform.localScale;

        while (elapsedTime < shrinkDuration)
        {
            transform.localScale = Vector3.Lerp(initialScale, Vector3.zero, elapsedTime / shrinkDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = Vector3.zero;
        Destroy(gameObject);
    }
}
