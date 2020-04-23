using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCombat : MonoBehaviour
{
    public UnitData unit;
    public bool attackingTile = false;

    private BuildingData buildingTarget;
    private float attackCooldownSec;
    private bool wasMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (attackingTile)
        {
            AttackBuilding();
        }
    }

    private void AttackBuilding()
    {
        if (Time.time > attackCooldownSec)
        {
            buildingTarget.UpdateHealth(-unit.unit.damage);
            attackCooldownSec = Time.time + unit.unit.attackDelaySec;

            if (buildingTarget.health <= 0)
            {
                attackingTile = false;
                if (wasMoving)
                {
                    unit.unitMovement.moving = true;
                }
                else if (unit.ownedByPlayer == null)
                {
                    if (GameData.thisPlayer.cities.Count > 0)
                    {
                        Vector2Int cityLocation = GameData.thisPlayer.cities[Random.Range(0, GameData.thisPlayer.cities.Count)].originLocation;
                        unit.unitMovement.SetDestanationGrid(cityLocation);
                    }
                }
            }
        }
    }

    public void SetBuildingTarget(BuildingData target, bool moving)
    {
        wasMoving = moving;
        unit.unitMovement.moving = false;
        buildingTarget = target;
        attackingTile = true;
    }
}
