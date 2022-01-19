using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathnode {
    
    private GridXZ<Pathnode> grid;
    public int x;
    public int z;

    public int gcost;
    public int hcost;
    public int fcost;

    public bool isWalkable;
    public Pathnode cameFromNode;
    public Pathnode(GridXZ<Pathnode> grid, int x, int z){
        this.grid = grid;
        this.x = x;
        this.z = z;
        isWalkable = true;
    }

    public void CalculateFCost(){
        fcost = gcost + hcost;
    }
    public void SetIsWalkable(bool isWalkable){
        this.isWalkable = isWalkable;
        grid.TriggergridObjectChanged(x, z);
    }
    public override string ToString()
    {
        return x + ", " + z;
    }
}
