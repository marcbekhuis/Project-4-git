using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlaceBuilding : MonoBehaviour
{
    public BuildingPrefab building;
    public bool destroysUnit = false;
    public UnitData unit;

    public void PlaceBuildingInGame()
    {
        GameData.buildingTilemap.SetTile((Vector3Int)unit.gridPosition, building.tile);
        GameObject spawnedBuilding = Instantiate(building.buildingScripts);

        GameData.buildings[unit.gridPosition.x, unit.gridPosition.y] = new BuildingData(building, spawnedBuilding, unit.gridPosition, unit.ownedByPlayer);
        spawnedBuilding.GetComponent<TownCenter>().buildingData = GameData.buildings[unit.gridPosition.x, unit.gridPosition.y];

        if (destroysUnit)
        {
            Destroy(GameData.activeActionPanel);
            GameData.selectedUnit = null;
            Destroy(unit.gameObject);
            GameData.units[unit.gridPosition.x,unit.gridPosition.y] = null;
        }
        else
        {
            Destroy(GameData.activeActionPanel);
            GameData.selectedUnit = null;
        }
    }
}
