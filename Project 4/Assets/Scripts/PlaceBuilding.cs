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
        UIElements.buildingsTilemap.SetTile((Vector3Int)unit.positionGrid, building.tile);
        GameObject spawnedBuilding = Instantiate(building.buildingScripts);
        spawnedBuilding.GetComponent<TownCenter>().gridPosition = unit.positionGrid;

        Buildings.buildings[unit.positionGrid.x, unit.positionGrid.y] = new BuildingData(building, spawnedBuilding, unit.positionGrid);

        if (destroysUnit)
        {
            Destroy(UIElements.activeUnitPanel);
            UIElements.selectedObject = null;
            Destroy(unit.gameObject);
            Units.units[unit.positionGrid.x,unit.positionGrid.y] = null;
        }
        else
        {
            Destroy(UIElements.activeUnitPanel);
            UIElements.selectedObject = null;
        }
    }
}
