using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public string PlayerName = "None";
    public List<CityData> cities = new List<CityData>();

    public List<int> citiesOverTime = new List<int>();


    float cooldownSec = 0;
    float delaySec = 60;
    private void Update()
    {
        if (Time.time > cooldownSec)
        {
            citiesOverTime.Add(cities.Count);

            cooldownSec = Time.time + delaySec;
        }
    }
}
