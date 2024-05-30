using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveTo : MonoBehaviour

{

    public GameObject target;
    public List<GameObject> targets;
    public int currentTarget = 0;
    private NavMeshAgent agent;
    private Animator animator;
    public float stopDistance = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        target = targets[currentTarget];
    }

    // Update is called once per frame
    void Update()
    {
        
        
        agent.SetDestination(target.transform.position);
        animator.SetBool("isRunning", true);

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