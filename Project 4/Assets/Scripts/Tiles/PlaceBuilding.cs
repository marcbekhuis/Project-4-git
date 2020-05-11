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
        if (GameData.buildings[unit.gridPosition.x, unit.gridPosition.y] != null)
        {
            if (GameData.buildings[unit.gridPosition.x, unit.gridPosition.y].building.townCenter)
            {
                return;
            }

            Destroy(GameData.buildings[unit.gridPosition.x, unit.gridPosition.y].scriptGameObject);
            GameData.thisPlayer.buildings.Remove(GameData.buildings[unit.gridPosition.x, unit.gridPosition.y]);
            
            if(GameData.buildings[unit.gridPosition.x, unit.gridPosition.y].building.maxNumberOfResidence > 0)
            {
                GameData.buildings[unit.gridPosition.x, unit.gridPosition.y].ownedByCity.residenceBuildings.Remove(GameData.buildings[unit.gridPosition.x, unit.gridPosition.y]);
            }
        }

        GameData.buildingTilemap.SetTile((Vector3Int)unit.gridPosition, building.tile);

        if (building.townCenter || building.producesResources)
        {
            GameObject spawnedBuilding = new GameObject();
            spawnedBuilding.name = building.name;

            GameData.buildings[unit.gridPosition.x, unit.gridPosition.y] = new BuildingData(building, unit.gridPosition, unit.ownedByPlayer, GameData.tiles[unit.gridPosition.x, unit.gridPosition.y].ownedByCity, spawnedBuilding);
            GameData.thisPlayer.buildings.Add(GameData.buildings[unit.gridPosition.x, unit.gridPosition.y]);

            TownCenter townCenter = new TownCenter();
            if (building.townCenter)
            {
                townCenter = spawnedBuilding.AddComponent<TownCenter>();
                townCenter.buildingData = GameData.buildings[unit.gridPosition.x, unit.gridPosition.y];
            }
            if (building.producesResources)
            {
                BuildingResourceGeneration buildingResourceGeneration = spawnedBuilding.AddComponent<BuildingResourceGeneration>();
                buildingResourceGeneration.buildingData = GameData.buildings[unit.gridPosition.x, unit.gridPosition.y];

            }

            if (building.maxNumberOfResidence > 0 && building.townCenter)
            {
                townCenter.cityData.residenceBuildings.Add(GameData.buildings[unit.gridPosition.x, unit.gridPosition.y]);
                townCenter.cityData.UpdateMaxPopulation();
            }
            else if (building.maxNumberOfResidence > 0)
            {
                GameData.tiles[unit.gridPosition.x, unit.gridPosition.y].ownedByCity.residenceBuildings.Add(GameData.buildings[unit.gridPosition.x, unit.gridPosition.y]);
                GameData.tiles[unit.gridPosition.x, unit.gridPosition.y].ownedByCity.UpdateMaxPopulation();
            }
        }
        else
        {
            GameData.buildings[unit.gridPosition.x, unit.gridPosition.y] = new BuildingData(building, unit.gridPosition, unit.ownedByPlayer, GameData.tiles[unit.gridPosition.x, unit.gridPosition.y].ownedByCity);
            GameData.thisPlayer.buildings.Add(GameData.buildings[unit.gridPosition.x, unit.gridPosition.y]);

            if (building.maxNumberOfResidence > 0)
            {
                GameData.tiles[unit.gridPosition.x, unit.gridPosition.y].ownedByCity.residenceBuildings.Add(GameData.buildings[unit.gridPosition.x, unit.gridPosition.y]);
                GameData.tiles[unit.gridPosition.x, unit.gridPosition.y].ownedByCity.UpdateMaxPopulation();
            }
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
