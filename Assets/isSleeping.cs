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
            animator.SetBool("isRunning", false);
        }
        else
        {
            animator.SetBool("isRunning", true);
            animator.SetBool("isSleeping", false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "EnergyPickup")
        {
            isColliding = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "EnergyPickup")
        {
            isColliding = false;
        }
    }
}
