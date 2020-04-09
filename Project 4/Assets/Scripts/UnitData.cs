using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitData
{
    public UnitData(UnitPrefab unit, BasicMovement basicMovement, GameObject gameObject, Vector2Int positionGrid)
    {
        this.unit = unit;
        this.basicMovement = basicMovement;
        this.gameObject = gameObject;
        this.positionGrid = positionGrid;
    }

    public UnitPrefab unit;
    public BasicMovement basicMovement;
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
        }
        else
        {
            UIElements.selectedObject = null;
        }
    }
}
