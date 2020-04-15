using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBeginUnits : MonoBehaviour
{
    [SerializeField] private GenerateMap generateMap;
    [SerializeField] private UnitPrefab settler;
    [SerializeField] private UnitPrefab worker;
    [SerializeField] private GameObject baseUnit;

    // Start is called before the first frame update
    void Start()
    {
        
        SpawnUnits();
    }

    private void SpawnUnits()
    {
        Units.units = new UnitData[generateMap.mapSize.x, generateMap.mapSize.y];
        Buildings.buildings = new BuildingData[generateMap.mapSize.x, generateMap.mapSize.y];

        Vector2Int arrayposition = new Vector2Int((int)(generateMap.mapSize.x * 0.5f), (int)(generateMap.mapSize.y * 0.4f));
        Vector2 position = HexagonCalculator.GridToHexagonPosition(arrayposition);
        Camera.main.transform.position = (Vector3)position - new Vector3(0,0,10);

        GameObject spawnedUnit = Instantiate(baseUnit, position, new Quaternion(0,0,0,0));
        spawnedUnit.GetComponent<SpriteRenderer>().sprite = settler.sprite;

        Units.units[arrayposition.x, arrayposition.y] = new UnitData(settler, spawnedUnit.GetComponent<UnitMovement>(), spawnedUnit, arrayposition);
        spawnedUnit.GetComponent<UnitMovement>().unitData = Units.units[arrayposition.x, arrayposition.y];


        arrayposition = arrayposition - new Vector2Int(1,0);
        position = HexagonCalculator.GridToHexagonPosition(arrayposition);

        spawnedUnit = Instantiate(baseUnit, position, new Quaternion(0, 0, 0, 0));
        spawnedUnit.GetComponent<SpriteRenderer>().sprite = worker.sprite;

        Units.units[arrayposition.x, arrayposition.y] = new UnitData(worker, spawnedUnit.GetComponent<UnitMovement>(), spawnedUnit, arrayposition);
        spawnedUnit.GetComponent<UnitMovement>().unitData = Units.units[arrayposition.x, arrayposition.y];
    }
}
