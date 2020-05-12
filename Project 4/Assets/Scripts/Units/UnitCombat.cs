using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCombat : MonoBehaviour
{
    public UnitData unit;
    public bool attackingTile = false;
    public bool attackingUnit = false;

    private BuildingData buildingTarget;
    private UnitData unitTarget;
    private Vector2Int unitTargetPosition;
    private float attackCooldownSec;
    private bool wasMoving = false;

    // Update is called once per frame
    void Update()
    {
        if (attackingUnit)
        {
            AttackUnit();
        }
        else if (attackingTile)
        {
            AttackBuilding();
        }
    }

    private void AttackBuilding()
    {
        if (Time.time > attackCooldownSec)
        {
            if (buildingTarget != null)
            {
                buildingTarget.UpdateHealth(-unit.unit.damage);
                attackCooldownSec = Time.time + unit.unit.attackDelaySec;

                if (buildingTarget.health <= 0)
                {
                    attackingTile = false;

                    unit.unitMovement.moving = wasMoving;

                    if (unit.ownedByPlayer == null)
                    {
                        if (GameData.thisPlayer.cities.Count > 0)
                        {
                            Vector2Int cityLocation = GameData.thisPlayer.cities[Random.Range(0, GameData.thisPlayer.cities.Count)].originLocation;
                            unit.unitMovement.SetDestanationGrid(cityLocation);
                            return;
                        }
                        else if (GameData.thisPlayer.units.Count > 0)
                        {
                            foreach (var unit in GameData.thisPlayer.units)
                            {
                                if (unit.unit.name == "Settler")
                                {
                                    unit.unitMovement.SetDestanationGrid(unit.gridPosition);
                                    return;
                                }
                            }

                            unit.unitMovement.SetDestanationGrid(GameData.thisPlayer.units[Random.Range(0, GameData.thisPlayer.units.Count)].gridPosition);
                            return;
                        }
                    }
                }
            }
            else
            {
                attackingTile = false;

                unit.unitMovement.moving = wasMoving;

                if (unit.ownedByPlayer == null)
                {
                    if (GameData.thisPlayer.cities.Count > 0)
                    {
                        Vector2Int cityLocation = GameData.thisPlayer.cities[Random.Range(0, GameData.thisPlayer.cities.Count)].originLocation;
                        unit.unitMovement.SetDestanationGrid(cityLocation);
                        return;
                    }
                    else if (GameData.thisPlayer.units.Count > 0)
                    {
                        foreach (var unit in GameData.thisPlayer.units)
                        {
                            if (unit.unit.name == "Settler")
                            {
                                unit.unitMovement.SetDestanationGrid(unit.gridPosition);
                                return;
                            }
                        }

                        unit.unitMovement.SetDestanationGrid(GameData.thisPlayer.units[Random.Range(0, GameData.thisPlayer.units.Count)].gridPosition);
                        return;
                    }
                }
            }
        }
    }

    private void AttackUnit()
    {
        if (unitTarget.gridPosition != unitTargetPosition)
        {
            unit.unitMovement.moving = wasMoving;
            return;
        }
        else if (Time.time > attackCooldownSec)
        {
            unitTarget.UpdateHealth(-unit.unit.damage);
            unit.UpdateHealth(-unitTarget.unit.damage);
            attackCooldownSec = Time.time + unit.unit.attackDelaySec;

            if (unitTarget.health <= 0)
            {
                attackingUnit = false;

                unit.unitMovement.moving = wasMoving;
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

    public void SetUnitTarget(UnitData target, bool moving, Vector2Int targetPosition)
    {
        wasMoving = moving;
        unit.unitMovement.moving = false;
        unitTarget = target;
        attackingUnit = true;
        unitTargetPosition = targetPosition;
    }
}
