using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityData
{
    public new string name = "New City";
    public Vector2Int originLocation;
    public string discription = "Intresting Discription";
    public int cityPopulation = 2;
    public int maxPopulation = 0;
    public List<Vector2> takenTiles = new List<Vector2>();
    public List<Vector2> availableUnits = new List<Vector2>();
    public List<Vector2> cityBuildings = new List<Vector2>();
    [HideInInspector] public List<BuildingData> residenceBuildings = new List<BuildingData>();

    public void UpdateMaxPopulation()
    {
        maxPopulation = 0;
        foreach (var residenceBuilding in residenceBuildings)
        {
            maxPopulation += residenceBuilding.building.maxNumberOfResidence;
        }
    }
    
    public void UpdatePopulation()
    {
        foreach (var item in GameData.thisPlayer.inventory.playerInventory)
        {
            if (item.name == "Bread")
            {
                int newAmount = Mathf.Clamp(item.amount, 0, maxPopulation);
                cityPopulation = newAmount;
                item.amount -= newAmount;
                return;
            }
        }
    }
}
