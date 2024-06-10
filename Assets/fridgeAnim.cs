using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fridgeAnim : MonoBehaviour
{
    public Animator animator; // Reference to the Animator component
    public string collisionAnimationBool = "isOpen"; // The name of the animation boolean parameter

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
            Debug.Log("Animator component found and assigned.");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player collided with the fridge.");
            if (animator != null)
            {
                animator.SetBool(collisionAnimationBool, true);
                Debug.Log("Animator bool set to true.");
            }
            else
            {
                Debug.LogError("Animator is null when trying to set bool.");
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        Debug.Log("Collision ended with: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player stopped colliding with the fridge.");
            if (animator != null)
            {
                animator.SetBool(collisionAnimationBool, false);
                Debug.Log("Animator bool set to false.");
            }
            else
            {
                Debug.LogError("Animator is null when trying to set bool.");
            }
        }
    }
}