using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedAnimation : MonoBehaviour
{
    public Animator animator; // Reference to the Animator component
    public string animationBool = "isOpen"; // The name of the animation boolean parameter

    void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        if (animator == null)
        {
            Debug.LogError("Animator component not found on the GameObject.");
        }
        else
        {
            StartCoroutine(PlayAnimationAfterDelay(17f));
        }
    }

    IEnumerator PlayAnimationAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (animator != null)
        {
            animator.SetBool(animationBool, true);
            Debug.Log("Animation triggered after delay.");
        }
        else
        {
            Debug.LogError("Animator is null when trying to set bool.");
        }
    }
}