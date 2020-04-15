using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitData
{
    public UnitData(UnitPrefab unit, UnitMovement unitMovement, GameObject gameObject, Vector2Int positionGrid)
    {
        this.unit = unit;
        this.unitMovement = unitMovement;
        this.gameObject = gameObject;
        this.positionGrid = positionGrid;
    }

    public UnitPrefab unit;
    public UnitMovement unitMovement;
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
            GameObject spawnedPanel = GameObject.Instantiate(unit.actionPanel, UIElements.canvas.transform);
            UIElements.activeUnitPanel = spawnedPanel;
            UIElements.selectedObject = gameObject;
            spawnedPanel.transform.Find("Move Unit").GetComponent<SelectUnitMoveToPos>().unitsData = this;
            if (spawnedPanel.transform.Find("Place Town center"))
            {
                PlaceBuilding placeBuilding = spawnedPanel.transform.Find("Place Town center").GetComponent<PlaceBuilding>();
                placeBuilding.unit = this;
            }
            if (spawnedPanel.transform.Find("Place Farm"))
            {
                PlaceBuilding placeBuilding = spawnedPanel.transform.Find("Place Farm").GetComponent<PlaceBuilding>();
                placeBuilding.unit = this;
            }
            if (spawnedPanel.transform.Find("Place House"))
            {
                PlaceBuilding placeBuilding = spawnedPanel.transform.Find("Place House").GetComponent<PlaceBuilding>();
                placeBuilding.unit = this;
            }
        }
        else
        {
            UIElements.selectedObject = null;
        }
    }
}
