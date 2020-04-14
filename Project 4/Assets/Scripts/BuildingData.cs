using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingData : MonoBehaviour
{
    public BuildingData(BuildingPrefab building, GameObject gameObject, Vector2Int positionGrid)
    {
        this.building = building;
        this.gameObject = gameObject;
        this.positionGrid = positionGrid;
    }

    public BuildingPrefab building;
    public GameObject gameObject;
    public Vector2Int positionGrid;

    public void OpenActionPanel()
    {
        if (UIElements.activeUnitPanel)
        {
            GameObject.Destroy(UIElements.activeUnitPanel);
        }
        if (UIElements.selectedObject != gameObject)
        {
            GameObject spawnedPanel = GameObject.Instantiate(building.actionPanel, UIElements.canvas.transform);
            UIElements.activeUnitPanel = spawnedPanel;
            UIElements.selectedObject = gameObject;
        }
        else
        {
            UIElements.selectedObject = null;
        }
    }
}
