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


    // Update is called once per frame
    private void Start()
    {
        agent.SetDestination(target.transform.position);
    }
    void Update()
    {
        if (target != null)
        {
            float dist = agent.remainingDistance;
            animator.SetBool("Walk", true);

            if (dist < 3)
            {
                animator.SetBool("Walk", false);
                agent.Stop();
            }

            if (Vector3.Distance(target.transform.position, gameObject.transform.position) < 3)
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
