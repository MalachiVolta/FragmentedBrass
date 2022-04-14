using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class GridBuildingSystem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI BuildablesText;
    [SerializeField] TextMeshProUGUI ErrorText;
    public Animator viewModelAnimator;
    public Weapon viewModel;

    public GameObject image;

    [SerializeField] private int WallCount = 5;
    [SerializeField] private int ScrapCount = 300;
    public static GridBuildingSystem Instance { get; private set; }
    [SerializeField] private List<PlacedObjectTypeSO> PlacableObjectsList;
    private PlacedObjectTypeSO PlacabaleObjects;
    private PlacedObjectTypeSO GhostObjects;

    public event EventHandler OnSelectedChanged;

    private GridXZ<GridObject> grid;
    private PlacedObjectTypeSO.Dir dir = PlacedObjectTypeSO.Dir.Down;

    public PlayerHandler player;
    [SerializeField] private int gridWidth = 10;
    [SerializeField] private int gridHeight = 10;
    [SerializeField] private float cellSize = 10f;
    private Vector3 OriginPosition;
    public NavMeshChecker check;

    private void Awake()
    {
        ErrorText.text = "";
        Instance = this;
        OriginPosition.x = transform.position.x;
        OriginPosition.y = transform.position.y;
        OriginPosition.z = transform.position.z;
        grid = new GridXZ<GridObject>(gridWidth, gridHeight, cellSize, OriginPosition, (GridXZ<GridObject> g, int x, int z) => new GridObject(g, x, z));

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
        BuildablesText.text = "Walls: " + WallCount.ToString() + "\n" + "Scrap: " + ScrapCount.ToString();
        try
        {

            if (Input.GetMouseButtonDown(0) && player.isBuilding)
            {

                if (WallCount <= 0)
                {
                    StartCoroutine(ErrorFlash("NOT ENOUGH RESOURCES!"));
                    viewModel.SoundBeepN();
                    return;
                }


                if (ScrapCount < 150 && PlacabaleObjects.isTurret)
                {
                    StartCoroutine(ErrorFlash("NOT ENOUGH RESOURCES!"));
                    viewModel.SoundBeepN();
                    return;
                }

                if (player.isBuilding)
                {
                    viewModelAnimator.SetTrigger("UseRadio");
                }
                else
                {
                    viewModelAnimator.ResetTrigger("UseRadio");
                }
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
                    Vector3 placedObjectWorldPosition = grid.GetWorldPosition(x, z) + new Vector3(rotationOffset.x, 0.1f, rotationOffset.y) * grid.GetCellSize();
                    PlacedObject placedObject = PlacedObject.Create(placedObjectWorldPosition, new Vector2Int(x, z), dir, PlacabaleObjects);
                    foreach (Vector2Int gridPosition in gridPositionList)
                    {
                        grid.GetGridObject(gridPosition.x, gridPosition.y).SetWall(placedObject);
                    }
                    gridObject.SetWall(placedObject);
                    viewModel.SoundBeepP();
                    if (!PlacabaleObjects.isTurret)
                    {
                        WallCount--;
                    }
                    else { WallCount--; ScrapCount -= 150; }
                }
                else
                {
                    StartCoroutine(ErrorFlash("CANNOT BUILD HERE!"));
                    viewModel.SoundBeepN();
                }

            }

            if (Input.GetMouseButtonDown(1) && player.isBuilding)
            {
                GridObject gridObject = grid.GetGridObject(Mouse3D.GetMouseWorldPosition());
                PlacedObject placedObject = gridObject.GetWall();

                if (placedObject == null)
                    return;

                if (!placedObject.isTurret)
                {
                    WallCount++;
                }
                else { WallCount++; ScrapCount += 150; }


                if (placedObject != null)
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
            if (Input.GetKeyDown(KeyCode.Alpha1) && player.isBuilding) { PlacabaleObjects = PlacableObjectsList[0]; GhostObjects = PlacableObjectsList[0]; }
            if (Input.GetKeyDown(KeyCode.Alpha2) && player.isBuilding) { PlacabaleObjects = PlacableObjectsList[1]; GhostObjects = PlacableObjectsList[1]; }
        }
        catch (Exception ex)
        {
            StartCoroutine(ErrorFlash("CANNOT BUILD HERE!"));
        }
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

    public void ReceiveWall(int Wall)
    {
        WallCount += Wall;
    }


    public void ReceiveTurret(int Scrap)
    {
        ScrapCount += Scrap;
    }

    IEnumerator ErrorFlash(string text)
    {
        ErrorText.text = text;
        image.SetActive(true);
        yield return new WaitForSeconds(2f);
        ErrorText.text = "";
        image.SetActive(false);
        yield return 0;


    }
}
