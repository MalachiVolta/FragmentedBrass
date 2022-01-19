using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GridBuildingSystem : MonoBehaviour
{
    public static GridBuildingSystem Instance { get; private set; }
    [SerializeField] private List<PlacedObjectTypeSO> PlacableObjectsList;
    private PlacedObjectTypeSO PlacabaleObjects;
    private PlacedObjectTypeSO GhostObjects;

    public event EventHandler OnSelectedChanged;

    private GridXZ<GridObject> grid;
    private PlacedObjectTypeSO.Dir dir = PlacedObjectTypeSO.Dir.Down;

    public PlayerHandler player;
    [SerializeField]private int gridWidth = 10;
    [SerializeField] private int gridHeight = 10;
    [SerializeField] private float cellSize = 10f;
    private Vector3 OriginPosition;
    public NavMeshChecker check;

    private void Awake()
    {
        Instance = this;
        OriginPosition.x = transform.position.x;
        OriginPosition.y = transform.position.y;
        OriginPosition.z = transform.position.z;
        grid = new GridXZ<GridObject>(gridWidth, gridHeight, cellSize, OriginPosition, (GridXZ<GridObject> g, int x, int z) => new GridObject(g,x,z));

        PlacabaleObjects = PlacableObjectsList[0];
        GhostObjects = PlacableObjectsList[0];
    }


    public class GridObject
    {
        private GridXZ<GridObject> grid;
        public int x;
        public int z;
        private Transform turret;
        private PlacedObject placedObject;

        public GridObject(GridXZ<GridObject> grid, int x, int z)
        {
            this.grid = grid;
            this.x = x;
            this.z = z;

        }

        public void SetWall(PlacedObject placedObject)
        {
            this.placedObject = placedObject;
            grid.TriggergridObjectChanged(x, z);
        }

        public PlacedObject GetWall()
        {
            return placedObject;
        }

        public void SetTurret(Transform turret)
        {
            this.turret = turret;
            grid.TriggergridObjectChanged(x, z);
        }

        public bool CanBuildWall()
        {
            return placedObject == null;
        }

        public bool CanBuildTurret()
        {
            return turret == null;
        }

        public void DestroyWall()
        {
            placedObject = null;
            grid.TriggergridObjectChanged(x, z);
        }

        public void DestroyTurret(Transform turret)
        {
            turret = null;
        }
    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0) && player.isBuilding)
        {
        grid.GetXZ(Mouse3D.GetMouseWorldPosition(), out int x, out int z);
            List<Vector2Int> gridPositionList = PlacabaleObjects.GetGridPositionList(new Vector2Int(x, z), dir);

            bool canBuild = true;
            foreach (Vector2Int gridPosition in gridPositionList)
            {
                if (!grid.GetGridObject(gridPosition.x, gridPosition.y).CanBuildWall())
                {
                    canBuild = false;
                    break;
                }
            }
            GridObject gridObject = grid.GetGridObject(x, z);

            bool makesure = check.checkBuildable();

            if (canBuild && makesure)
            {
                Vector2Int rotationOffset = PlacabaleObjects.GetRotationOffset(dir);
                Vector3 placedObjectWorldPosition = grid.GetWorldPosition(x, z) + new Vector3(rotationOffset.x, 0, rotationOffset.y) * grid.GetCellSize();
                PlacedObject placedObject = PlacedObject.Create(placedObjectWorldPosition, new Vector2Int(x,z), dir, PlacabaleObjects);
                foreach (Vector2Int gridPosition in gridPositionList)
                {
                    grid.GetGridObject(gridPosition.x, gridPosition.y).SetWall(placedObject);
                }
                gridObject.SetWall(placedObject);
            }
            else
            {
                Debug.Log("Cantbuildhere");
            }

        }

        if(Input.GetMouseButtonDown(1) && player.isBuilding)
        {
            GridObject gridObject = grid.GetGridObject(Mouse3D.GetMouseWorldPosition());
            PlacedObject placedObject = gridObject.GetWall();
            if(placedObject != null)
            {
                placedObject.DestroySelf();

                List<Vector2Int> gridPositionList = placedObject.GetGridPositionList();

                foreach (Vector2Int gridPosition in gridPositionList)
                {
                    grid.GetGridObject(gridPosition.x, gridPosition.y).DestroyWall();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.R) && player.isBuilding)
        {
            dir = PlacedObjectTypeSO.GetNextDir(dir);
        }
        if(Input.GetKeyDown(KeyCode.Alpha1) && player.isBuilding) { PlacabaleObjects = PlacableObjectsList[0];}
        if (Input.GetKeyDown(KeyCode.Alpha2) && player.isBuilding) { PlacabaleObjects = PlacableObjectsList[1]; }
        if (Input.GetKeyDown(KeyCode.Alpha3) && player.isBuilding) { PlacabaleObjects = PlacableObjectsList[2]; }
        if (Input.GetKeyDown(KeyCode.Alpha4) && player.isBuilding) { PlacabaleObjects = PlacableObjectsList[3]; }

    }
    private void RefreshSelectedObjectType()
    {
        OnSelectedChanged?.Invoke(this, EventArgs.Empty);
    }

    public Vector3 GetMouseWorldSnappedPosition()
    {
        grid.GetXZ(Mouse3D.GetMouseWorldPosition(), out int x, out int z);
        List<Vector2Int> gridPositionList = GhostObjects.GetGridPositionList(new Vector2Int(x, z), dir); ;

        if (GhostObjects != null)
        {
            Vector2Int rotationOffset = GhostObjects.GetRotationOffset(dir);
            Vector3 placedObjectWorldPosition = grid.GetWorldPosition(x, z) + new Vector3(rotationOffset.x, 0, rotationOffset.y) * grid.GetCellSize();
            return placedObjectWorldPosition;
        }
        else
        {
            return Vector3.zero;
        }
    }

    public Quaternion GetPlacedObjectRotation()
    {
        if (GhostObjects != null)
        {
            return Quaternion.Euler(0, GhostObjects.GetRotationAngle(dir), 0);
        }
        else
        {
            return Quaternion.identity;
        }
    }

    public PlacedObjectTypeSO GetPlacedObjectTypeSO()
    {
        return GhostObjects;
    }

}