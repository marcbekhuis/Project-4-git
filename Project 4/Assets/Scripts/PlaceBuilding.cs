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
            GameData.buildingTilemap.SetTile((Vector3Int)unit.gridPosition, building.tile);

            if (building.buildingScripts)
            {
                GameObject spawnedBuilding = Instantiate(building.buildingScripts);

                GameData.buildings[unit.gridPosition.x, unit.gridPosition.y] = new BuildingData(building, spawnedBuilding, unit.gridPosition, unit.ownedByPlayer);
                spawnedBuilding.GetComponent<TownCenter>().buildingData = GameData.buildings[unit.gridPosition.x, unit.gridPosition.y];
            }
            else
            {
                GameData.buildings[unit.gridPosition.x, unit.gridPosition.y] = new BuildingData(building, null, unit.gridPosition, unit.ownedByPlayer);
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
        else if (!building.mustBePlaceInClaim)
        {
            GameData.buildingTilemap.SetTile((Vector3Int)unit.gridPosition, building.tile);
            GameObject spawnedBuilding = Instantiate(building.buildingScripts);

            GameData.buildings[unit.gridPosition.x, unit.gridPosition.y] = new BuildingData(building, spawnedBuilding, unit.gridPosition, unit.ownedByPlayer);
            spawnedBuilding.GetComponent<TownCenter>().buildingData = GameData.buildings[unit.gridPosition.x, unit.gridPosition.y];

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
}
