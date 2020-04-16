using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerData : MonoBehaviour
{
    public string PlayerName = "None";
    [HideInInspector] public int population = 0;

    [HideInInspector] public List<CityData> cities = new List<CityData>();
    [HideInInspector] public List<UnitData> units = new List<UnitData>();
    [HideInInspector] public List<TileData> claimedTiles = new List<TileData>();
    public TileBase border;

    [HideInInspector] public GameData.TileVisibility[,] tileVisibility;

    [HideInInspector] public Inventory inventory;

    [HideInInspector] public List<int> citiesOverTime = new List<int>();
    [HideInInspector] public List<int> unitsOverTime = new List<int>();
    [HideInInspector] public List<int> populationOverTime = new List<int>();

    float cooldownSec = 0;
    float delaySec = 60;

    private void Update()
    {
        if (Time.time > cooldownSec)
        {
            population = 0;
            foreach (var city in cities)
            {
                population += city.cityPopulation;
            }

            citiesOverTime.Add(cities.Count);
            unitsOverTime.Add(units.Count);
            populationOverTime.Add(population);

            cooldownSec = Time.time + delaySec;
        }
    }
}
