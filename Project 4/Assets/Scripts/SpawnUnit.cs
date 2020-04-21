using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnUnit : MonoBehaviour
{
    public BuildingData buildingData;
    public UnitPrefab unitPrefab;
    public GameObject baseUnit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnU()
    {
        Vector2 position = HexagonCalculator.GridToHexagonPosition(buildingData.gridPosition);
        GameObject spawnedUnit = Instantiate(baseUnit, position, new Quaternion(0, 0, 0, 0));
        spawnedUnit.GetComponent<SpriteRenderer>().sprite = unitPrefab.sprite;

        GameData.units[buildingData.gridPosition.x, buildingData.gridPosition.y] = new UnitData(unitPrefab, spawnedUnit.GetComponent<UnitMovement>(), spawnedUnit, buildingData.gridPosition, GameData.thisPlayer);
        spawnedUnit.GetComponent<UnitMovement>().unitData = GameData.units[buildingData.gridPosition.x, buildingData.gridPosition.y];
        GameData.thisPlayer.units.Add(GameData.units[buildingData.gridPosition.x, buildingData.gridPosition.y]);

        GameData.fogOfWar.UpdateVisibility();
    }
}
