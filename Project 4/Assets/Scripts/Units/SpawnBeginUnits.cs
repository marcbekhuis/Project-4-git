﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBeginUnits : MonoBehaviour
{
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
        GameData.units = new UnitData[GameData.mapSize.x, GameData.mapSize.y];
        GameData.buildings = new BuildingData[GameData.mapSize.x, GameData.mapSize.y];

        Vector2Int arrayposition = new Vector2Int((int)(GameData.mapSize.x * 0.5f), (int)(GameData.mapSize.y * 0.4f));
        Vector2 position = HexagonCalculator.GridToHexagonPosition(arrayposition);
        Camera.main.transform.position = (Vector3)position - new Vector3(0,0,10);

        GameObject spawnedUnit = Instantiate(baseUnit, position, new Quaternion(0,0,0,0));
        spawnedUnit.GetComponent<SpriteRenderer>().sprite = settler.sprite;

        UnitMovement unitMovement = spawnedUnit.GetComponent<UnitMovement>();
        UnitCombat unitCombat = spawnedUnit.GetComponent<UnitCombat>();

        GameData.units[arrayposition.x, arrayposition.y] = new UnitData(settler, unitMovement, spawnedUnit, arrayposition, GameData.thisPlayer, unitCombat);
        unitMovement.unitData = GameData.units[arrayposition.x, arrayposition.y];
        unitCombat.unit = GameData.units[arrayposition.x, arrayposition.y];

        GameData.thisPlayer.units.Add(GameData.units[arrayposition.x, arrayposition.y]);

        arrayposition = arrayposition - new Vector2Int(1,0);
        position = HexagonCalculator.GridToHexagonPosition(arrayposition);

        spawnedUnit = Instantiate(baseUnit, position, new Quaternion(0, 0, 0, 0));
        spawnedUnit.GetComponent<SpriteRenderer>().sprite = worker.sprite;

        unitMovement = spawnedUnit.GetComponent<UnitMovement>();
        unitCombat = spawnedUnit.GetComponent<UnitCombat>();

        GameData.units[arrayposition.x, arrayposition.y] = new UnitData(worker, unitMovement, spawnedUnit, arrayposition, GameData.thisPlayer, unitCombat);
        unitMovement.unitData = GameData.units[arrayposition.x, arrayposition.y];
        unitCombat.unit = GameData.units[arrayposition.x, arrayposition.y];

        GameData.thisPlayer.units.Add(GameData.units[arrayposition.x, arrayposition.y]);

        GameData.fogOfWar.UpdateVisibility();
    }
}
