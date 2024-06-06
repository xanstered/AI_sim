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
        if (other.CompareTag("bed"))
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("isSleeping", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("bed"))
        {
            animator.SetBool("isSleeping", false);
        }
    }
}
