using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class barsAndAnims : MonoBehaviour
{
    public Image energyBar;
    public Image foodBar;
    public Image hygieneBar;
    public Image funBar;

    public float decreaseRate = 0.1f;
    public float increaseRate = 0.1f; // Rate at which bars fill when colliding

    public float moveSpeed = 5f;

    private NavMeshAgent navMeshAgent;
    private int currentTargetIndex = 0;

    private Animator animator;

    public GameObject target;
    public List<GameObject> targets;
    public int currentTarget = 0;
    public float stopDistance = 0.1f;

    private void Start()
    {
        // Initialize the fill amount of all bars to 1.0 (full)
        energyBar.fillAmount = 1.0f;
        foodBar.fillAmount = 1.0f;
        hygieneBar.fillAmount = 1.0f;
        funBar.fillAmount = 1.0f;

        navMeshAgent = GetComponent<NavMeshAgent>();

        animator = GetComponent<Animator>();

        target = targets[currentTarget];
    }

    private void Update()
    {
        // Decrease the fill amount of each bar over time
        DecreaseBarFillAmount(energyBar);
        DecreaseBarFillAmount(foodBar);
        DecreaseBarFillAmount(hygieneBar);
        DecreaseBarFillAmount(funBar);

        // Check if any of the bars are empty, and move to the target position
        if (energyBar.fillAmount == 0 || foodBar.fillAmount == 0 || hygieneBar.fillAmount == 0 || funBar.fillAmount == 0)
        {
            MoveToTargetPosition();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the character collides with the objects representing the bars
        if (other.CompareTag("EnergyPickup"))
        {
            IncreaseBarFillAmount(energyBar);

            animator.SetBool("isRunning", false);
            animator.SetBool("isSleeping", true);
        }
        else if (other.CompareTag("FoodPickup"))
        {
            IncreaseBarFillAmount(foodBar);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("HygienePickup"))
        {
            IncreaseBarFillAmount(hygieneBar);
        }
        else if (other.CompareTag("FunPickup"))
        {
            IncreaseBarFillAmount(funBar);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("bed"))
        {
            animator.SetBool("isSleeping", false);
        }
    }

    private void DecreaseBarFillAmount(Image bar)
    {
        // Decrease the fill amount of the bar based on the decrease rate
        bar.fillAmount -= decreaseRate * Time.deltaTime;

        // Ensure the fill amount doesn't go below 0
        if (bar.fillAmount < 0)
        {
            bar.fillAmount = 0;
        }
    }

    private void IncreaseBarFillAmount(Image bar)
    {
        // Increase the fill amount of the bar based on the increase rate
        bar.fillAmount += increaseRate;

        // Ensure the fill amount doesn't exceed 1
        if (bar.fillAmount > 1)
        {
            bar.fillAmount = 1;
        }
    }

    private void MoveToTargetPosition()
    {
        if (Vector3.Distance(transform.position, target.transform.position) < 0.1f)
        {
            animator.SetBool("isRunning", false);
            currentTarget++;

            if (currentTarget >= targets.Count)
            {
                currentTarget = 0;
            }

            target = targets[currentTarget];
        }
        else
        {
            animator.SetBool("isRunning", true);
        }
    }
}
