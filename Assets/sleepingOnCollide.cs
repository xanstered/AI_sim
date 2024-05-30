using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sleepingOnCollide : MonoBehaviour
{
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("bed"))
        {

            animator.SetBool("isSleeping", true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("bed"))
        {
            animator.SetBool("isRunning", false);
        }
    }
}
