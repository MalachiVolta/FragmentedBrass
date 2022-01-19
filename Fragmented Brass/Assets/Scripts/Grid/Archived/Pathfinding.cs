/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GridBuildingSystem;

public class Pathfinding {
    
    private const int MOVE_STRAIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14;

    private GridXZ<GridObject> gridXZ;
    private List<GridObject> openList;
    private List<GridObject> closedList;

    public Pathfinding(int width, int height, float size,Vector3 origin){
        gridXZ = new GridXZ<GridObject>(width, height, size, origin, (GridXZ<GridObject> g, int x, int z) => new GridObject(g, x, z));
    }

    public Pathfinding(GridXZ<GridObject> BuildingSystem)
    {
        gridXZ = BuildingSystem;
    }

    public GridXZ<GridObject> GetGrid(){
        return gridXZ;
    }

    public List<GridObject> FindPath(int startX, int startZ, int endX, int endZ){
        GridObject startNode = gridXZ.GetGridObject(startX,startZ);
        GridObject endNode = gridXZ.GetGridObject(endX,endZ);

        if (startNode == null || endNode == null) {
            // Invalid Path
            return null;
        }

        openList = new List<GridObject>{ startNode };
        closedList = new List<GridObject>();

        
        for (int x = 0; x < gridXZ.GetWidth();x++){
            for (int z = 0; z < gridXZ.GetHeight(); z++){
                GridObject pathNode = gridXZ.GetGridObject(x,z);
                pathNode.gcost = 99999999;
                pathNode.CalculateFCost();
                pathNode.cameFromNode = null;
            }
        }

        startNode.gcost = 0;
        startNode.hcost = CalculateDistanceCost(startNode, endNode);
        startNode.CalculateFCost();

        while (openList.Count > 0){
            GridObject currentNode = GetLowestFCostNode(openList);
            if (currentNode == endNode) {
                return CalculatePath(endNode);
            }
             
             openList.Remove(currentNode);
             closedList.Add(currentNode);

            foreach (GridObject neighbourNode in GetNeighbourList(currentNode)){
                if (closedList.Contains(neighbourNode)) continue;
                if (!neighbourNode.isWalkable){
                    closedList.Add(neighbourNode);
                    continue;
                }

                int tentativeGCost = currentNode.gcost + CalculateDistanceCost(currentNode, neighbourNode);
                if (tentativeGCost < neighbourNode.gcost){
                    neighbourNode.cameFromNode = currentNode;
                    neighbourNode.gcost = tentativeGCost;
                    neighbourNode.hcost = CalculateDistanceCost(neighbourNode,endNode);
                    neighbourNode.CalculateFCost();
                    
                    if (!openList.Contains(neighbourNode)){
                        openList.Add(neighbourNode);
                    }
                }
            }
        }

        //Out of nodes on the openList
        return null;
    }

    private List<GridObject> GetNeighbourList(GridObject currentNode){
        List<GridObject> neighbourList = new List<GridObject>();

        if(currentNode.x - 1 >= 0){
            //left
            neighbourList.Add(GetNode(currentNode.x - 1, currentNode.z));
            //Left Down
            if (currentNode.z - 1 >= 0) neighbourList.Add(GetNode(currentNode.x - 1, currentNode.z - 1));
            //Left Up
            if (currentNode.z - 1 < gridXZ.GetHeight()) neighbourList.Add(GetNode(currentNode.x - 1, currentNode.z + 1));
        }
        if(currentNode.x + 1 < gridXZ.GetWidth()){
            //Right
            neighbourList.Add(GetNode(currentNode.x + 1, currentNode.z));
            //Right Down
            if (currentNode.z - 1 >= 0) neighbourList.Add(GetNode(currentNode.x + 1, currentNode.z - 1));
            //Right Up
            if (currentNode.z - 1 < gridXZ.GetHeight()) neighbourList.Add(GetNode(currentNode.x + 1, currentNode.z + 1));
        }
        //Down
        if (currentNode.z - 1 >= 0) neighbourList.Add(GetNode(currentNode.x, currentNode.z - 1));
        //Up
        if (currentNode.z - 1 < gridXZ.GetHeight()) neighbourList.Add(GetNode(currentNode.x, currentNode.z + 1));

        return neighbourList;
    }

    public GridObject GetNode(int x, int z) {
        return gridXZ.GetGridObject(x, z);
    }

    private List<GridObject> CalculatePath(GridObject endNode){
        List<GridObject> path = new List<GridObject>();
        path.Add(endNode);
        GridObject currentNode = endNode;
        while (currentNode.cameFromNode != null){
            path.Add(currentNode.cameFromNode);
            currentNode = currentNode.cameFromNode;
        }
        path.Reverse();
        return path;
    }

    private int CalculateDistanceCost(GridObject a, GridObject b){
        int xDistance = Mathf.Abs(a.x - b.x);
        int zDistance = Mathf.Abs(a.z - b.z);
        int remaining = Mathf.Abs(xDistance - zDistance);
        return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, zDistance) + MOVE_STRAIGHT_COST * remaining;
    }

    private GridObject GetLowestFCostNode(List<GridObject> pathNodeList){
        GridObject lowestFCostNode = pathNodeList[0];
        for (int i = 1; i < pathNodeList.Count; i++){
            if (pathNodeList[i].fcost < lowestFCostNode.fcost){
                lowestFCostNode = pathNodeList[i];
                
            }
        }
        return lowestFCostNode;
    }
}*/
