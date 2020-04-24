using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    public bool moving = false;
    public float speed = 1;

    public Queue<Vector2Int> path = new Queue<Vector2Int>();
    Vector2Int destanationGridPosition;
    public UnitData unitData;

    Vector2Int nextTileGridPosition;
    Vector2Int previousTileGridPosition;
    float distanceBetweenTiles = 0;
    bool movedOnGrid = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveFromTileToTile();
    }

    public void SetDestanation(Vector2 worldPositon)
    {
        destanationGridPosition = HexagonCalculator.HexagonToGridPosition(worldPositon);
        CalculatePath();
    }

    public void SetDestanationGrid(Vector2Int gridPositon)
    {
        destanationGridPosition = gridPositon;
        CalculatePath();
    }

    private void CalculatePath()
    {
        moving = false;
        path.Clear();
        Vector2Int lastPosition = unitData.gridPosition;
        //Debug.LogError("First last position: " + lastPosition);
        int nextMoveToPositionY = lastPosition.y;
        int nextMoveToPositionX = lastPosition.x;

        while (lastPosition != destanationGridPosition)
        {
            if (lastPosition.y % 2 == 1)
            {
                if (lastPosition.x < destanationGridPosition.x && lastPosition.y < destanationGridPosition.y)
                {
                    nextMoveToPositionX = lastPosition.x + 1;
                    nextMoveToPositionY = lastPosition.y + 1;
                }
                else if (lastPosition.x < destanationGridPosition.x && lastPosition.y > destanationGridPosition.y)
                {
                    nextMoveToPositionY = lastPosition.y - 1;
                    nextMoveToPositionX = lastPosition.x + 1;
                }
                else if (lastPosition.y < destanationGridPosition.y)
                {
                    nextMoveToPositionY = lastPosition.y + 1;
                }
                else if (lastPosition.y > destanationGridPosition.y)
                {
                    nextMoveToPositionY = lastPosition.y - 1;
                }
                else if (lastPosition.x < destanationGridPosition.x)
                {
                    nextMoveToPositionX = lastPosition.x + 1;
                }
                else if (lastPosition.x > destanationGridPosition.x)
                {
                    nextMoveToPositionX = lastPosition.x - 1;
                }
            }
            else
            {
                if (lastPosition.x > destanationGridPosition.x && lastPosition.y < destanationGridPosition.y)
                {
                    nextMoveToPositionX = lastPosition.x - 1;
                    nextMoveToPositionY = lastPosition.y + 1;
                }
                else if (lastPosition.x > destanationGridPosition.x && lastPosition.y > destanationGridPosition.y)
                {
                    nextMoveToPositionY = lastPosition.y - 1;
                    nextMoveToPositionX = lastPosition.x - 1;
                }
                else if (lastPosition.x < destanationGridPosition.x)
                {
                    nextMoveToPositionX = lastPosition.x + 1;
                }
                else if (lastPosition.x > destanationGridPosition.x)
                {
                    nextMoveToPositionX = lastPosition.x - 1;
                }
                else if (lastPosition.y < destanationGridPosition.y)
                {
                    nextMoveToPositionY = lastPosition.y + 1;
                }
                else if (lastPosition.y > destanationGridPosition.y)
                {
                    nextMoveToPositionY = lastPosition.y - 1;
                }
            }

            Vector2Int nextMoveToPosition = new Vector2Int(nextMoveToPositionX, nextMoveToPositionY);
            lastPosition = nextMoveToPosition;

            path.Enqueue(nextMoveToPosition);
        }

        //Debug.LogError("Path length: " + path.Count);

        nextTileGridPosition = path.Dequeue();
        previousTileGridPosition = unitData.gridPosition;
        distanceBetweenTiles = 0;
        movedOnGrid = false;
        moving = true;

        if (GameData.units[nextTileGridPosition.x, nextTileGridPosition.y] != null)
        {
            if (GameData.units[nextTileGridPosition.x, nextTileGridPosition.y].ownedByPlayer == unitData.ownedByPlayer)
            {
                moving = false;
            }
            else
            {
                unitData.unitCombat.SetUnitTarget(GameData.units[nextTileGridPosition.x, nextTileGridPosition.y], moving, nextTileGridPosition);
            }
        }
    }

    private void MoveFromTileToTile()
    {
        if (moving)
        {
            distanceBetweenTiles += Time.deltaTime / speed;
            //Debug.LogError("distance: " + distanceBetweenTiles);
            unitData.gameObject.transform.position = Vector2.Lerp(HexagonCalculator.GridToHexagonPosition(previousTileGridPosition), HexagonCalculator.GridToHexagonPosition(nextTileGridPosition), distanceBetweenTiles);

            if (distanceBetweenTiles > 0.5 && distanceBetweenTiles < 0.6 && !movedOnGrid)
            {
                movedOnGrid = true;
                GameData.units[previousTileGridPosition.x, previousTileGridPosition.y] = null;
                GameData.units[nextTileGridPosition.x, nextTileGridPosition.y] = unitData;
                unitData.gridPosition = nextTileGridPosition;
            }
            else if (distanceBetweenTiles >= 1)
            {
                GameData.units[previousTileGridPosition.x, previousTileGridPosition.y] = null;
                GameData.units[nextTileGridPosition.x, nextTileGridPosition.y] = unitData;
                unitData.gridPosition = nextTileGridPosition;
                unitData.gameObject.transform.position = HexagonCalculator.GridToHexagonPosition(nextTileGridPosition);
                distanceBetweenTiles = 0;

                previousTileGridPosition = unitData.gridPosition;
                movedOnGrid = false;

                if (unitData.ownedByPlayer)
                {
                    GameData.fogOfWar.UpdateVisibility();
                }

                if (path.Count == 0)
                {
                    //Debug.Log("Finsihed moving");
                    moving = false;

                    if (GameData.buildings[unitData.gridPosition.x, unitData.gridPosition.y] != null)
                    {
                        if (GameData.buildings[unitData.gridPosition.x, unitData.gridPosition.y].ownedByPlayer != unitData.ownedByPlayer)
                        {
                            if (unitData.unit.damage > 0)
                            {
                                unitData.unitCombat.SetBuildingTarget(GameData.buildings[unitData.gridPosition.x, unitData.gridPosition.y], moving);
                            }
                        }
                    }
                    return;
                }

                    nextTileGridPosition = path.Dequeue();
                //Debug.LogError("Path length: " + path.Count);

                if (GameData.units[nextTileGridPosition.x, nextTileGridPosition.y] != null)
                {
                    if (GameData.units[nextTileGridPosition.x, nextTileGridPosition.y].ownedByPlayer == unitData.ownedByPlayer)
                    {
                        moving = false;
                    }
                    else
                    {
                        unitData.unitCombat.SetUnitTarget(GameData.units[nextTileGridPosition.x, nextTileGridPosition.y], moving, nextTileGridPosition);
                    }
                }

                if (GameData.buildings[unitData.gridPosition.x, unitData.gridPosition.y] != null)
                {
                    if (GameData.buildings[unitData.gridPosition.x, unitData.gridPosition.y].ownedByPlayer != unitData.ownedByPlayer)
                    {
                        if (unitData.unit.damage > 0)
                        {
                            if (Random.Range(0f, 100f) < 50f)
                            {
                                unitData.unitCombat.SetBuildingTarget(GameData.buildings[unitData.gridPosition.x, unitData.gridPosition.y], moving);
                            }
                        }
                    }
                }
            }
        }
    }
}
