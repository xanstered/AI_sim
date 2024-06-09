using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fridgeAnim : MonoBehaviour
{
    public Animator animator; // The Animator component attached to the player sprite
    public string animationTriggerName = "isOpen"; // The name of the trigger parameter in the Animator

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
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            animator.SetTrigger("isOpen");
        }
    }
}
