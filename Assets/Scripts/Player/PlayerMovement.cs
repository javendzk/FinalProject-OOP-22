using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 4f;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Transform playerVisual;
    public bool isSwimming = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerVisual = transform.Find("PlayerVisual");
        spriteRenderer = playerVisual.GetComponent<SpriteRenderer>();
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
            spriteRenderer.flipX = moveX < 0;
            float angle;

            if (moveX != 0)
            {
                angle = moveY > 0 ? (spriteRenderer.flipX ? -45 : 45) : (moveY < 0 ? (spriteRenderer.flipX ? 45 : -45) : 0);
            }
            else
            {
                angle = moveY > 0 ? (spriteRenderer.flipX ? -90 : 90) : (moveY < 0 ? (spriteRenderer.flipX ? 90 : -90) : 0);
            }

            playerVisual.rotation = Quaternion.Euler(0, 0, angle);
        }
        else
        {
            playerVisual.rotation = Quaternion.Euler(0, 0, 0);
        }

        isSwimming = movement.magnitude > 0;
    }

    public bool IsSwimming()
    {
        return isSwimming;
    }
}
