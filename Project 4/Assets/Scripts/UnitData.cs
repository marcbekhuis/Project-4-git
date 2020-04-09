using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitData
{
    public UnitData(UnitPrefab unit, BasicMovement basicMovement, GameObject gameObject)
    {
        this.unit = unit;
        this.basicMovement = basicMovement;
        this.gameObject = gameObject;
    }

    public UnitPrefab unit;
    public BasicMovement basicMovement;
    public GameObject gameObject;

    public void OpenActionPanel()
    {
        if (UIElements.activeUnitPanel)
        {
            GameObject.Destroy(UIElements.activeUnitPanel);
        }
        GameObject spawnedPanel = GameObject.Instantiate(unit.actionPanel, UIElements.canvas.transform);
        UIElements.activeUnitPanel = spawnedPanel;
    }
}
