using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitData
{
    public UnitData(UnitPrefab unit, UnitMovement unitMovement, GameObject gameObject, Vector2Int gridPosition, PlayerData OwnedByPlayer, UnitCombat unitCombat)
    {
        this.unit = unit;
        this.unitMovement = unitMovement;
        this.gameObject = gameObject;
        this.gridPosition = gridPosition;
        ownedByPlayer = OwnedByPlayer;
        health = unit.maxHealth;
        this.unitCombat = unitCombat;
    }

    public UnitPrefab unit;
    public UnitMovement unitMovement;
    public UnitCombat unitCombat;
    public GameObject gameObject;
    public Vector2Int gridPosition;
    public PlayerData ownedByPlayer;
    public float health;

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

    public void UpdateHealth(float amount)
    {
        health += amount;

        if (health <= 0)
        {
            if (ownedByPlayer)
            {
                ownedByPlayer.units.Remove(this);
            }
            GameObject.Destroy(gameObject);
            GameData.units[gridPosition.x, gridPosition.y] = null;
        }
    }
}
