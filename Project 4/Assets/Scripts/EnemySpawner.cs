﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private UnitPrefab[] unitsToSpawn;
    [SerializeField] private GameObject unitBase;

    [SerializeField] private int unitsPerWave = 1;
    [SerializeField] private float spawnDelaySec = 80;
    [SerializeField] private float beginDelaySec = 320;

    private float spawnCooldownSec = 0;

    // Start is called before the first frame update
    void Start()
    {
        spawnCooldownSec = Time.time + beginDelaySec;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerData.cities.Count > 0)
        {
            if (Time.time > spawnCooldownSec)
            {
                for (int i = 0; i < unitsPerWave; i++)
                {
                    Vector2 cityLocation = playerData.cities[Random.Range(0, playerData.cities.Count)].originLocation;
                    Vector2 position = HexagonCalculator.GridToHexagonPosition(cityLocation);
                    position = position + (Vector2)(Quaternion.Euler(0, 0, Random.Range(0f, 360f)) * new Vector3(10,0, 0));

                    Vector2Int gridPosition = HexagonCalculator.HexagonToGridPosition(position);
                    position = HexagonCalculator.GridToHexagonPosition(gridPosition);

                    GameObject spawnedUnit = Instantiate(unitBase, position, new Quaternion(0,0,0,0));
                    UnitPrefab unit = unitsToSpawn[Random.Range(0, unitsToSpawn.Length)];
                    spawnedUnit.GetComponent<SpriteRenderer>().sprite = unit.sprite;
                    UnitMovement unitMovement = spawnedUnit.GetComponent<UnitMovement>();
                    Units.units[gridPosition.x, gridPosition.y] = new UnitData(unit, unitMovement, spawnedUnit, gridPosition);
                    unitMovement.unitData = Units.units[gridPosition.x, gridPosition.y];
                    unitMovement.SetDestanationGrid(new Vector2Int((int)cityLocation.x, (int)cityLocation.y));

                    spawnCooldownSec = Time.time + spawnDelaySec;
                }
            }
        }
    }
}
