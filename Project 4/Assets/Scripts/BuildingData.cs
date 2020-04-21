using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingData
{
    public BuildingData(BuildingPrefab building, Vector2Int gridPosition, PlayerData OwnedByPlayer, CityData OwnedByCity, GameObject ScriptGameObject = null)
    {
        this.building = building;
        this.scriptGameObject = ScriptGameObject;
        this.gridPosition = gridPosition;
        ownedByPlayer = OwnedByPlayer;
        ownedByCity = OwnedByCity;
    }

    public BuildingPrefab building;
    public GameObject scriptGameObject;
    public Vector2Int gridPosition;
    public PlayerData ownedByPlayer;
    public CityData ownedByCity;

    public void OpenActionPanel()
    {
        if (GameData.activeActionPanel)
        {
            GameObject.Destroy(GameData.activeActionPanel);
        }
        if (GameData.selectedBuilding != this)
        {
            GameObject spawnedPanel = GameObject.Instantiate(building.actionPanel, GameData.canvas.transform);

            foreach (var spawnUnit in spawnedPanel.GetComponentsInChildren<SpawnUnit>())
            {
                spawnUnit.buildingData = this;
            }

            GameData.activeActionPanel = spawnedPanel;
            GameData.selectedBuilding = this;
            GameData.selectedUnit = null;
        }
        else
        {
            GameData.selectedBuilding = null;
            GameData.selectedUnit = null;
        }
    }
}
