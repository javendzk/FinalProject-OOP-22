using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class menuButton : MonoBehaviour
{
    [SerializeField] menuController menuControllerReference;
    [SerializeField] Animator animator;

    [SerializeField] int thisIndex;

    private bool start = true;

    void Start()
    {
        if (thisIndex == 1) { start = false; }
    }

    void Update()
    {
        if (menuControllerReference.index == thisIndex)
        {
            animator.SetBool("selected", true);
            if (Input.GetAxis("Submit") == 1)
            {
                animator.SetBool("pressed", true);
                if (start)
                {
                    menuControllerReference.PlayGame();
                }
                else
                {
                    menuControllerReference.QuitGame();
                }
            }
            else if (animator.GetBool("pressed"))
            {
                animator.SetBool("pressed", false);
            }
        }
        else
        {
            animator.SetBool("selected", false);
        }
    }
}