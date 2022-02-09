/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour{
    //[SerializeField] private HeatMapVisual heatMapVisual;
    [SerializeField] private HeatMapBoolVisual heatMapBoolVisual;
    [SerializeField] private HeatMapGenericVisual heatMapGenericVisual;
    private Grid<HeatMapGridObject> grid;

    private void Start() {
        grid = new Grid<HeatMapGridObject>(20,10, 10f, Vector3.zero, (Grid<HeatMapGridObject> g, int x, int y) => new HeatMapGridObject(g, x, y));
        heatMapGenericVisual.SetGrid(grid);
        //heatMapVisual.SetGrid(grid);
        //heatMapBoolVisual.SetGrid(grid);
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)){
            Vector3 position = UtilsClass.GetMouseWorldPosition();
            HeatMapGridObject heatMapGridObject = grid.GetGridObject(position);
            if (heatMapGridObject != null){
                heatMapGridObject.AddValue(5);
            }
            //grid.SetValue(position, true);
            //grid.AddValue(position, 100, 5, 40);
        }
    }
}

public class HeatMapGridObject{

    private const int MIN = 0;
    private const int MAX = 100;
    
    private Grid<HeatMapGridObject> grid;
    private int x;
    private int y;
    private int value;

    public HeatMapGridObject(Grid<HeatMapGridObject> grid, int x, int y){
        this.grid = grid;
        this.x = x;
        this.y = y;
    }

    public void AddValue(int addValue){
        value += addValue;
        value = Mathf.Clamp(value, MIN, MAX);
        grid.TriggergridObjectChanged(x,y);
    }

    public float GetValueNormalized(){
        return (float)value / MAX;
    }

    public override string ToString(){
        return value.ToString();
    }
}*/
