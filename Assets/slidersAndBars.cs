using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class slidersAndBars : MonoBehaviour
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

    public float decreaseRate = 1f;
    public float increaseRate = 20f;

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
        foodBar.value -= decreaseRate * Time.deltaTime;
        hygieneBar.value -= decreaseRate * Time.deltaTime;
        funBar.value -= decreaseRate * Time.deltaTime;
        energyBar.value -= decreaseRate * Time.deltaTime;
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
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform == foodTarget && currentNeed == "food")
        {
            StartCoroutine(IncreaseBarOverTime(foodBar));
        }
        else if (other.transform == hygieneTarget && currentNeed == "hygiene")
        {
            StartCoroutine(IncreaseBarOverTime(hygieneBar));
        }
        else if (other.transform == funTarget && currentNeed == "fun")
        {
            StartCoroutine(IncreaseBarOverTime(funBar));
        }
        else if (other.transform == energyTarget && currentNeed == "energy")
        {
            StartCoroutine(IncreaseBarOverTime(energyBar));
        }
    }

    IEnumerator IncreaseBarOverTime(Slider bar)
    {
        agent.isStopped = true;

        while (bar.value < bar.maxValue)
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
}
