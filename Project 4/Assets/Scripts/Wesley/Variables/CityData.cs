using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityData : MonoBehaviour
{
    public new string name = "New City";
    public Vector2 originLocation = new Vector2(0, 0);
    public string discription = "Intresting Discription";
    public int cityPopulation = 1;
    public List<Vector2> takenTiles = new List<Vector2>();
    public List<Vector2> availableUnits = new List<Vector2>();
    public List<Vector2> cityBuildings = new List<Vector2>();
    public Inventory inventory = null;

    public void SetNewCity(string cityName, Vector2 capitalLocation, string cityDiscription, int startPopulation, Inventory cityInventory)
    {
        name = cityName;
        originLocation = capitalLocation;
        discription = cityDiscription;
        cityPopulation = startPopulation;
        inventory = cityInventory;
    }

    public CityData CreateNewCity(string cityName, Vector2 capitalLocation, string cityDiscription, int startPopulation, Inventory cityInventory)
    {
        CityData newCity = new CityData();

        newCity.SetNewCity("New City", new Vector2(0, 0), "Intresting Discription", 1, null);

        return newCity;
    }
}
