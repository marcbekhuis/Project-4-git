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

        UnitMovement unitMovement = spawnedUnit.GetComponent<UnitMovement>();
        UnitCombat unitCombat = spawnedUnit.GetComponent<UnitCombat>();

        GameData.units[buildingData.gridPosition.x, buildingData.gridPosition.y] = new UnitData(unitPrefab, unitMovement, spawnedUnit, buildingData.gridPosition, GameData.thisPlayer,unitCombat);
        unitMovement.unitData = GameData.units[buildingData.gridPosition.x, buildingData.gridPosition.y];
        unitCombat.unit = GameData.units[buildingData.gridPosition.x, buildingData.gridPosition.y];

        GameData.thisPlayer.units.Add(GameData.units[buildingData.gridPosition.x, buildingData.gridPosition.y]);

        GameData.fogOfWar.UpdateVisibility();

        Destroy(GameData.activeActionPanel);
        GameData.selectedBuilding = null;
        GameData.selectedUnit = null;
    }
}
