using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIScipt : MonoBehaviour
{

    public NavMeshAgent agent;
    public Animator animator;
    public GameObject target;
    public Target enemy;
    private NavMeshPath path;


    // Update is called once per frame
    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Target");
        enemy = target.GetComponent<Target>();
        agent.autoRepath = true;

        NavMeshPath path = new NavMeshPath();
        agent.CalculatePath(target.transform.position, path);
        agent.SetPath(path);

    }
    void Update()
    {
        if (agent.pathStatus == NavMeshPathStatus.PathInvalid)
        {
            agent.ResetPath();
            agent.CalculatePath(target.transform.position, path);
            agent.SetPath(path);
        }

        if (target != null)
        {
            float dist = agent.remainingDistance;
            animator.SetBool("Walk", true);

            if (dist < 3)
            {
                animator.SetBool("Walk", false);
                agent.isStopped = true;
            }

            if (Vector3.Distance(target.transform.position, gameObject.transform.position) < 4)
            {
                animator.SetTrigger("Attack");
            }
        }
    }

    void AttackTarget()
    {
        if (target != null)
            enemy.Hit(20);
    }
}
