using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityData : MonoBehaviour
{
    public new string name = "New City";
    public Vector2 originLocation = new Vector2(0, 0);
    public string discription = "Intresting Discription";
    public int cityPopulation = 2;
    public int maxPopulation = 0;
    public List<Vector2> takenTiles = new List<Vector2>();
    public List<Vector2> availableUnits = new List<Vector2>();
    public List<Vector2> cityBuildings = new List<Vector2>();
    [HideInInspector] public List<BuildingData> residenceBuildings = new List<BuildingData>();

    public void SetNewCity(string cityName, Vector2 capitalLocation, string cityDiscription, int startPopulation)
    {
        name = cityName;
        originLocation = capitalLocation;
        discription = cityDiscription;
        cityPopulation = startPopulation;
    }

    public CityData CreateNewCity(string cityName, Vector2 capitalLocation, string cityDiscription, int startPopulation)
    {
        CityData newCity = new CityData();

        newCity.SetNewCity("New City", new Vector2(0, 0), "Intresting Discription", 1);

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
        Item wheat = new Item();
        wheat.name = "Wheat";

        Item wheatInventory = new Item();

        foreach (var item in GameData.thisPlayer.inventory.playerInventory)
        {
            if (item.name == wheat.name)
            {
                wheatInventory = item;
                break;
            }
        }

        int newAmount = Mathf.Clamp(wheatInventory.amount, 0, maxPopulation);
        cityPopulation = newAmount;
        wheatInventory.amount -= newAmount;
    }
}
