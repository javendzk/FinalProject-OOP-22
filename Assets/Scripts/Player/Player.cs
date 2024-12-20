using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private Animator animator;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        animator = transform.Find("PlayerVisual").GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        playerMovement.Move();
    }

    void LateUpdate()
    {
        bool isSwimming = playerMovement.IsSwimming();
        animator.SetBool("isSwimming", isSwimming);
        if (isSwimming)
        {
            animator.Play("PlayerSwim");
        }
    }
}
