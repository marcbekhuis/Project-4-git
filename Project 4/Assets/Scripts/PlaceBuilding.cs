using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlaceBuilding : MonoBehaviour
{
    public BuildingPrefab building;
    public UnitData unit;

    public void PlaceBuildingInGame()
    {
        if (building.mustBePlaceInClaim && GameData.tiles[unit.gridPosition.x, unit.gridPosition.y].ownedByPlayer == GameData.thisPlayer)
        {
            PlaceBuildingInGame2();
        }
        else if (!building.mustBePlaceInClaim)
        {
            PlaceBuildingInGame2();
        }
    }

    private void PlaceBuildingInGame2()
    {
        GameData.buildingTilemap.SetTile((Vector3Int)unit.gridPosition, building.tile);

        if (building.townCenter || building.producesResources)
        {
            GameObject spawnedBuilding = new GameObject();
            spawnedBuilding.name = building.name;

            GameData.buildings[unit.gridPosition.x, unit.gridPosition.y] = new BuildingData(building, unit.gridPosition, unit.ownedByPlayer, spawnedBuilding);

            if (building.townCenter)
            {
                TownCenter townCenter = spawnedBuilding.AddComponent<TownCenter>();
                townCenter.buildingData = GameData.buildings[unit.gridPosition.x, unit.gridPosition.y];
            }
            if (building.producesResources)
            {
                BuildingResourceGeneration buildingResourceGeneration = spawnedBuilding.AddComponent<BuildingResourceGeneration>();
                buildingResourceGeneration.buildingData = GameData.buildings[unit.gridPosition.x, unit.gridPosition.y];

            }
        }
        else
        {
            GameData.buildings[unit.gridPosition.x, unit.gridPosition.y] = new BuildingData(building, unit.gridPosition, unit.ownedByPlayer);
        }

        if (building.destroysUnit)
        {
            Destroy(GameData.activeActionPanel);
            GameData.selectedUnit = null;
            Destroy(unit.gameObject);
            GameData.units[unit.gridPosition.x, unit.gridPosition.y] = null;
            GameData.thisPlayer.units.Remove(unit);
            GameData.fogOfWar.UpdateVisibility();
        }
        else
        {
            Destroy(GameData.activeActionPanel);
            GameData.selectedUnit = null;
        }
    }
}
