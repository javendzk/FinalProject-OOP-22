using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    public bool isSwimming = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Move();
    }

    public void Move()
    {
        float moveX = Input.GetAxisRaw("Horizontal"); 
        float moveY = Input.GetAxisRaw("Vertical");   

        Vector2 movement = new Vector2(moveX, moveY).normalized;
        rb.velocity = movement * moveSpeed;

        if (movement.magnitude > 0)
        {
            float angle = 0;
            if (moveX > 0)
            {
                spriteRenderer.flipX = false;
                angle = moveY > 0 ? 45 : (moveY < 0 ? -45 : 0);
            }
            else if (moveX < 0)
            {
                spriteRenderer.flipX = true;
                angle = moveY > 0 ? -45 : (moveY < 0 ? 45 : 0);
            }
            else
            {
                angle = moveY > 0 ? 90 : (moveY < 0 ? -90 : 0);
            }
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        isSwimming = movement.magnitude > 0;
    }

    public bool IsSwimming()
    {
        return isSwimming;
    }
}
