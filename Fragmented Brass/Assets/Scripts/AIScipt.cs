using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIScipt : MonoBehaviour
{

    public NavMeshAgent agent;
    public GameObject target;
    public Enemy enemy;
    private RaycastHit hit;

    // Update is called once per frame
    void Update()
    {
        
        agent.SetDestination(target.transform.position);

        if(Vector3.Distance(target.transform.position, gameObject.transform.position)<3)
        {
            enemy.Hit(20);
        }

    }
}
