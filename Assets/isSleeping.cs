using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isSleeping : MonoBehaviour
{
    private Animator animator;
    private bool isColliding = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isColliding)
        {
            animator.SetBool("isSleeping", true);
        }
        else
        {
            animator.SetBool("isRunning", true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "bed")
        {
            isColliding = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "bed")
        {
            isColliding = false;
        }
    }
}
