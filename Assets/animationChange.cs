using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationChange : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("energy"))
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("isSleeping", true);
        }

        if(other.CompareTag("food"))
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("pickingUp", true);
        }

        if (other.CompareTag("hygiene"))
        {
            animator.SetBool("isRunning", false);
        }

        if (other.CompareTag("fun"))
        {
            animator.SetBool("isRunning", false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("energy"))
        {
            animator.SetBool("isSleeping", false);
        }

        if (other.CompareTag("food"))
        {
            animator.SetBool("pickingUp", false);
        }

        if (other.CompareTag("hygiene"))
        {
            animator.SetBool("isRunning", true);
        }

        if (other.CompareTag("fun"))
        {
            animator.SetBool("isRunning", true);
        }
    }
}
