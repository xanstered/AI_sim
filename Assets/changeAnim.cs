using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeAnim : MonoBehaviour
{

    [SerializeField] private Animator animator;

    private void OnTriggerEnter(Collider other)
    {
            if (other.gameObject.CompareTag("bed"))
            {
                animator.SetBool("isSleeping", true);
                animator.SetBool("isRunning", false);
            }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("bed"))
        {
            animator.SetBool("isSleeping", false);

            animator.SetBool("isRunning", true);
        }
    }
}
