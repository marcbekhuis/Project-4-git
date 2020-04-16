using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingData : MonoBehaviour
{
    public BuildingData(BuildingPrefab building, GameObject gameObject, Vector2Int gridPosition, PlayerData OwnedByPlayer)
    {
        this.building = building;
        this.gameObject = gameObject;
        this.gridPosition = gridPosition;
        ownedByPlayer = OwnedByPlayer;
    }

    public BuildingPrefab building;
    public GameObject gameObject;
    public Vector2Int gridPosition;
    public PlayerData ownedByPlayer;

    public void OpenActionPanel()
    {
        if (GameData.activeActionPanel)
        {
            GameObject.Destroy(GameData.activeActionPanel);
        }
        if (GameData.selectedBuilding != this)
        {
            GameObject spawnedPanel = GameObject.Instantiate(building.actionPanel, GameData.canvas.transform);
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
