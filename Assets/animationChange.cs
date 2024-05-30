using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationChange : MonoBehaviour
{
    private Animator animator;

    // Animation state names
    private string normalAnimation = "isRunning";
    private string collisionAnimation = "isSleeping";

    void Start()
    {
        // Get the Animator component attached to the player
        animator = GetComponent<Animator>();
    }

    // This method is called when the Collider other enters the trigger
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object collided with has a specific tag (e.g., "Obstacle")
        if (other.CompareTag("bed"))
        {
            // Change the animation to the collision state
            animator.Play(collisionAnimation);
        }
    }

    // This method is called when the Collider other exits the trigger
    private void OnTriggerExit(Collider other)
    {
        // Check if the object exited is the same type (e.g., "Obstacle")
        if (other.CompareTag("bed"))
        {
            // Change the animation back to the normal state
            animator.Play(normalAnimation);
        }
    }
}
