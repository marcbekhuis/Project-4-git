using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitData
{
    public UnitData(Vector2 Position, UnitPrefab unit)
    {
        position = Position;
        this.unit = unit;
    }

    public UnitPrefab unit;
    public Vector2 position;

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
