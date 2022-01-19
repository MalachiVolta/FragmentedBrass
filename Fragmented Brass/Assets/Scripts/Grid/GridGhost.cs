using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGhost : MonoBehaviour
{

    private Transform visual;
    private PlacedObjectTypeSO placedObjectTypeSO;
    public PlayerHandler player;

    private void Start()
    {
        RefreshVisual();

        GridBuildingSystem.Instance.OnSelectedChanged += Instance_OnSelectedChanged;
    }

    private void Instance_OnSelectedChanged(object sender, System.EventArgs e)
    {
        RefreshVisual();
    }

    private void LateUpdate()
    {
        if (player.isBuilding)
        {
            RefreshVisual();
            Vector3 targetPosition = GridBuildingSystem.Instance.GetMouseWorldSnappedPosition();
            targetPosition.y = 0f;
            transform.position = targetPosition;

            transform.rotation = GridBuildingSystem.Instance.GetPlacedObjectRotation();
        }
        else if (visual != null && !player.isBuilding)
        {
            Destroy(visual.gameObject);
            visual = null;
        }

    }

    private void RefreshVisual()
    {
        if (visual != null)
        {
            Destroy(visual.gameObject);
            visual = null;
        }

        PlacedObjectTypeSO placedObjectTypeSO = GridBuildingSystem.Instance.GetPlacedObjectTypeSO();

        if (placedObjectTypeSO != null)
        {
            visual = Instantiate(placedObjectTypeSO.visual, Vector3.zero, Quaternion.identity);
            visual.parent = transform;
            visual.localPosition = Vector3.zero;
            visual.localEulerAngles = Vector3.zero;
            SetLayerRecursive(visual.gameObject, 11);
        }
    }

    private void SetLayerRecursive(GameObject targetGameObject, int layer)
    {
        targetGameObject.layer = layer;
        foreach (Transform child in targetGameObject.transform)
        {
            SetLayerRecursive(child.gameObject, layer);
        }
    }



}

