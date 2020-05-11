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

    public void SetNewCity(string cityName, Vector2Int capitalLocation, string cityDiscription, int startPopulation)
    {
        name = cityName;
        originLocation = capitalLocation;
        discription = cityDiscription;
        cityPopulation = startPopulation;
    }

    public CityData CreateNewCity(string cityName, Vector2 capitalLocation, string cityDiscription, int startPopulation)
    {
        CityData newCity = new CityData();

        newCity.SetNewCity("New City", Vector2Int.zero, "Intresting Discription", 1);

        return newCity;
    }

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
        Item bread = new Item();
        bread.name = "Bread";

        Item breadInventory = new Item();

        foreach (var item in GameData.thisPlayer.inventory.playerInventory)
        {
            if (item.name == bread.name)
            {
                breadInventory = item;
                break;
            }
        }

        int newAmount = Mathf.Clamp(breadInventory.amount, 0, maxPopulation);
        cityPopulation = newAmount;
        breadInventory.amount -= newAmount;
    }
}
