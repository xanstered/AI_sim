using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class newAnim : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform foodTarget;
    public Transform hygieneTarget;
    public Transform funTarget;
    public Transform energyTarget;

    public Slider foodBar;
    public Slider hygieneBar;
    public Slider funBar;
    public Slider energyBar;

    public float foodDecreaseRate = 1f;
    public float hygieneDecreaseRate = 1f;
    public float funDecreaseRate = 1f;
    public float energyDecreaseRate = 1f;
    public float increaseRate = 20f;

    public Animator animator;

    private bool isMovingToTarget = false;
    private string currentNeed = "";


    void Start()
    {
        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }

        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        ResetBars();
    }

    void Update()
    {
        DecreaseBarsOverTime();

        if (!isMovingToTarget)
        {
            CheckBarsAndMoveToTarget();
        }
    }

    void DecreaseBarsOverTime()
    {
        foodBar.value -= foodDecreaseRate * Time.deltaTime;
        hygieneBar.value -= hygieneDecreaseRate * Time.deltaTime;
        funBar.value -= funDecreaseRate * Time.deltaTime;
        energyBar.value -= energyDecreaseRate * Time.deltaTime;
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
            StartCoroutine(IncreaseBarOverTime(foodBar, "pickingUp"));
        }
        else if (other.transform == hygieneTarget && currentNeed == "hygiene")
        {
            StartCoroutine(IncreaseBarOverTime(hygieneBar, "isSitting"));
        }
        else if (other.transform == funTarget && currentNeed == "fun")
        {
            StartCoroutine(IncreaseBarOverTime(funBar, "isSitting"));
        }
        else if (other.transform == energyTarget && currentNeed == "energy")
        {
            StartCoroutine(IncreaseBarOverTime(energyBar, "isSleeping"));
        }
    }

    IEnumerator IncreaseBarOverTime(Slider bar, string animationTrigger)
    {
        agent.isStopped = true;
        animator.SetBool("isRunning", false);
        animator.SetBool(animationTrigger, true);

        while (bar.value < bar.maxValue)
        {
            bar.value += increaseRate * Time.deltaTime;
            yield return null;
        }

        animator.SetBool(animationTrigger, false);
        agent.isStopped = false;
        isMovingToTarget = false;
    }

    void ResetBars()
    {
        foodBar.value = foodBar.maxValue;
        hygieneBar.value = hygieneBar.maxValue;
        funBar.value = funBar.maxValue;
        energyBar.value = energyBar.maxValue;
    }
}