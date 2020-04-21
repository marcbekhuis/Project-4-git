using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingResourceGeneration : MonoBehaviour
{
    [HideInInspector] public BuildingData buildingData;
    private float cooldownSec;

    // Start is called before the first frame update
    void Start()
    {
        cooldownSec = Time.time + buildingData.building.productionDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > cooldownSec)
        {
            GameData.thisPlayer.inventory.PickUpItem(buildingData.building.itemToProduce, buildingData.building.amount);
            cooldownSec = Time.time + buildingData.building.productionDelay;
        }
    }
}
