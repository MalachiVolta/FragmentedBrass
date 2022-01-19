/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using CodeMonkey;
using static GridBuildingSystem;

public class TestingPathfinding : MonoBehaviour
{
    private Pathfinding pathfinding;

    private void Start()
    {
        pathfinding = new Pathfinding(12, 10, 2f, Vector3.zero);

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPosition = Mouse3D.GetMouseWorldPosition();
            Debug.Log(mouseWorldPosition);
            pathfinding.GetGrid().GetXZ(mouseWorldPosition, out int x, out int z);
            List<GridObject> path = pathfinding.FindPath(0, 0, x, z);
            if (path != null)
            {
                for (int i = 0; i < path.Count - 1; i++)
                {
                    Debug.Log("Ping");
                    Debug.DrawLine(new Vector3(path[i].x, path[i].z) * 10f + Vector3.one * 5f, new Vector3(path[i + 1].x, path[i + 1].z) * 10f + Vector3.one * 5f, Color.green, 5f);
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPositionWithZ();
            pathfinding.GetGrid().GetXZ(mouseWorldPosition, out int x, out int z);
            pathfinding.GetNode(x, z).SetIsWalkable(!pathfinding.GetNode(x, z).isWalkable);
        }
    }

}
*/