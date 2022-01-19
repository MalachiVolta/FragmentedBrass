using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshChecker : MonoBehaviour
{
    private NavMeshAgent agent;
    private NavMeshPath path;
    public GameObject target;

    private void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
    }

    public bool checkBuildable()
    {
        NavMeshPath path = new NavMeshPath();
        agent.CalculatePath(target.transform.position, path);
        Debug.Log(path.status);
        if (path.status == NavMeshPathStatus.PathComplete)
            return true;
        else
            return false;
    }
}
