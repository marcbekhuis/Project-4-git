using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public string PlayerName = "None";
    public List<CityData> cities = new List<CityData>();
    public List<UnitData> units = new List<UnitData>();
    public int population = 0;

    public List<int> citiesOverTime = new List<int>();
    public List<int> unitsOverTime = new List<int>();
    public List<int> populationOverTime = new List<int>();

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
