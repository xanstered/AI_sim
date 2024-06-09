using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class slidersAndBars : MonoBehaviour
{
    public NavMeshAgent agent;

    private Animator animator;

    public Transform foodTarget;
    public Transform hygieneTarget;
    public Transform funTarget;
    public Transform energyTarget;

    public Slider foodBar;
    public Slider hygieneBar;
    public Slider funBar;
    public Slider energyBar;

    public float FoodDecreaseRate = 1f;

    public float HygieneDecreaseRate = 1.5f;

    public float FunDecreaseRate = 0.2f;
    
    public float EnergyDecreaseRate = 0.25f;

    public float increaseRate = 1.2f;

    private bool isMovingToTarget = false;
    private string currentNeed = "";

    public float maxValue = 10f;


    void Start()
    {
        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }

        ResetBars();

        animator = GetComponent<Animator>();
    }

    void Update()
    {
        DecreaseBarsOverTime();

        if (!isMovingToTarget)
        {
            CheckBarsAndMoveToTarget();
        }

        if (foodBar.value >= maxValue)
        {
            animator.SetBool("pickingUp", false);
        }

        if (energyBar.value >= maxValue)
        {
            animator.SetBool("isSleeping", false);
        }

        if (hygieneBar.value >= maxValue)
        {
            animator.SetBool("pickingUp", false);
        }

        if (funBar.value >= maxValue)
        {
            animator.SetBool("pickingUp", false);
        }
    }

    void DecreaseBarsOverTime()
    {
        foodBar.value -= FoodDecreaseRate * Time.deltaTime;
        hygieneBar.value -= HygieneDecreaseRate * Time.deltaTime;
        funBar.value -= FunDecreaseRate * Time.deltaTime;
        energyBar.value -= EnergyDecreaseRate * Time.deltaTime;
    }

    void CheckBarsAndMoveToTarget()
    {
        if (foodBar.value <= 0)
        {
            MoveToTarget(foodTarget, "food");
        }
        else if (hygieneBar.value <= 0)
        {
            MoveToTarget(hygieneTarget, "hygiene");
        }
        else if (funBar.value <= 0)
        {
            MoveToTarget(funTarget, "fun");
        }
        else if (energyBar.value <= 0)
        {
            MoveToTarget(energyTarget, "energy");
        }
    }

    void MoveToTarget(Transform target, string need)
    {
        isMovingToTarget = true;
        currentNeed = need;
        agent.SetDestination(target.position);

        animator.SetBool("isRunning", true);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform == foodTarget && currentNeed == "food")
        {
            StartCoroutine(IncreaseBarOverTime(foodBar));

            animator.SetBool("pickingUp", true);
            animator.SetBool("isRunning", false);
        }

        else if (other.transform == hygieneTarget && currentNeed == "hygiene")
        {
            StartCoroutine(IncreaseBarOverTime(hygieneBar));

            animator.SetBool("isSitting", true);
            animator.SetBool("isRunning", false);
        }

        else if (other.transform == funTarget && currentNeed == "fun")
        {
            StartCoroutine(IncreaseBarOverTime(funBar));

            animator.SetBool("isSitting", true);
            animator.SetBool("isRunning", false);
        }

        else if (other.transform == energyTarget && currentNeed == "energy")
        {
            StartCoroutine(IncreaseBarOverTime(energyBar));

            animator.SetBool("isSleeping", true);
            animator.SetBool("isRunning", false);
        }

    }

    IEnumerator IncreaseBarOverTime(Slider bar)
    {
        agent.isStopped = true;

        yield return new WaitForSeconds(1);

        while (bar.value <= bar.maxValue)
        {
            bar.value += increaseRate * Time.deltaTime;
            yield return null;
        }

        agent.isStopped = false;
        isMovingToTarget = false;
    }

    void ResetBars()
    {
        foodBar.value = maxValue;
        hygieneBar.value = maxValue;
        funBar.value = maxValue;
        energyBar.value = maxValue;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("energy"))
        {
            animator.SetBool("isSleeping", false);
        }

        if (other.CompareTag("food"))
        {
            animator.SetBool("pickingUp", false);
        }

        if (other.CompareTag("hygiene"))
        {
            animator.SetBool("isSitting", false);
        }

        if (other.CompareTag("fun"))
        {
            animator.SetBool("isSitting", false);
        }
    }

}
