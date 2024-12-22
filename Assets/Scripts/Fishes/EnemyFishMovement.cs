using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFishMovement : MonoBehaviour
{
    public float slowSpeed = 1f;
    public float fastSpeed = 3.5f;
    public float detectionRange = 10f;
    public float guardRange = 8f;
    public float returnDuration = 1f; 
    public Transform player;
    private Rigidbody2D rb;
    private Vector2 movement;
    private Vector2 initialPosition;
    private BoxCollider2D boxCollider;
    private Vector2 minGuardRange;
    private Vector2 maxGuardRange;

    private enum State { Guarding, Chasing, Returning }
    private State currentState;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        boxCollider = GetComponent<BoxCollider2D>();
        initialPosition = transform.position;
        minGuardRange = initialPosition - new Vector2(guardRange / 2, guardRange / 2);
        maxGuardRange = initialPosition + new Vector2(guardRange / 2, guardRange / 2);
        currentState = State.Guarding;
        StartCoroutine(RandomMovement());
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (currentState != State.Returning)
        {
            if (distanceToPlayer < detectionRange)
            {
                currentState = State.Chasing;
                ChasePlayer();
            }
            else
            {
                currentState = State.Guarding;
                ReturnToGuardPosition();
            }
        }

        FlipSprite();
    }

    IEnumerator RandomMovement()
    {
        while (true)
        {
            if (currentState == State.Guarding)
            {
                float speed = slowSpeed;
                Vector2 randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
                Vector2 targetPosition = initialPosition + randomDirection * Random.Range(0, guardRange / 2);

                if (Vector2.Distance(initialPosition, targetPosition) <= guardRange)
                {
                    while (Vector2.Distance(transform.position, targetPosition) > 0.1f && currentState == State.Guarding)
                    {
                        movement = (targetPosition - (Vector2)transform.position).normalized * speed;
                        rb.velocity = movement;
                        FlipSprite();
                        yield return null;
                    }
                }
            }
            yield return null;
        }
    }

    void ChasePlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * fastSpeed;
    }

    public IEnumerator ReturnToGuardSpotAndChase()
    {
        currentState = State.Returning;
        Vector2 direction = (initialPosition - (Vector2)transform.position).normalized;

        rb.velocity = direction * slowSpeed; 
        yield return new WaitForSeconds(returnDuration);

        if (this == null)
        {
            yield break;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer < detectionRange)
        {
            currentState = State.Chasing;
            ChasePlayer();
        }
        else
        {
            currentState = State.Guarding;
            ReturnToGuardPosition();
        }
    }

    void ReturnToGuardPosition() 
    {
        Vector2 direction = Vector2.zero;

        if (transform.position.x < minGuardRange.x)
        {
            direction.x = 1;
        }
        else if (transform.position.x > maxGuardRange.x)
        {
            direction.x = -1;
        }

        if (transform.position.y < minGuardRange.y)
        {
            direction.y = 1;
        }
        else if (transform.position.y > maxGuardRange.y)
        {
            direction.y = -1;
        }

        rb.velocity = direction.normalized * slowSpeed;
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
}
