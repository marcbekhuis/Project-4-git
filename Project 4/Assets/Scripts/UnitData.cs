using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitData
{
    public UnitData(UnitPrefab unit, UnitMovement unitMovement, GameObject gameObject, Vector2Int gridPosition, PlayerData OwnedByPlayer)
    {
        this.unit = unit;
        this.unitMovement = unitMovement;
        this.gameObject = gameObject;
        this.gridPosition = gridPosition;
        ownedByPlayer = OwnedByPlayer;
    }

    public UnitPrefab unit;
    public UnitMovement unitMovement;
    public GameObject gameObject;
    public Vector2Int gridPosition;
    public PlayerData ownedByPlayer;

    public void OpenActionPanel()
    {
        if (GameData.activeActionPanel)
        {
            GameObject.Destroy(GameData.activeActionPanel);
        }
        if (GameData.selectedUnit != this)
        {
            GameObject spawnedPanel = GameObject.Instantiate(unit.actionPanel, GameData.canvas.transform);
            GameData.activeActionPanel = spawnedPanel;
            GameData.selectedBuilding = null;
            GameData.selectedUnit = this;

            spawnedPanel.transform.Find("Move Unit").GetComponent<SelectUnitMoveToPos>().unitsData = this;
            foreach (var placeBuilding in spawnedPanel.GetComponentsInChildren<PlaceBuilding>())
            {
                placeBuilding.unit = this;
            }
        }
        else
        {
            GameData.selectedBuilding = null;
            GameData.selectedUnit = null;
        }
    }
}
