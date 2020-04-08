using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBeginUnits : MonoBehaviour
{
    [SerializeField] private GenerateMap generateMap;
    [SerializeField] private UnitPrefab settler;
    [SerializeField] private GameObject baseUnit;

    // Start is called before the first frame update
    void Start()
    {
        SpawnUnits();
    }

    private void SpawnUnits()
    {
        Units.units = new UnitData[generateMap.mapSize.x, generateMap.mapSize.y];

        Vector2Int arrayposition = generateMap.mapSize / 2;
        Vector2 position = HexagonCalculator.WorldToHexagonPosition(arrayposition);

        Units.units[arrayposition.x, arrayposition.y] = new UnitData(position, settler);
        GameObject spawnedUnit = Instantiate(baseUnit, position, new Quaternion(0,0,0,0));
        spawnedUnit.GetComponent<SpriteRenderer>().sprite = settler.sprite;
    }
}
