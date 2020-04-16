using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    public bool moving = false;
    public float speed = 1;

    Queue<Vector2Int> path = new Queue<Vector2Int>();
    Vector2Int destanationGridPosition;
    public UnitData unitData;

    Vector2Int nextTileGridPosition;
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
        for (int i = unitData.gridPosition.y; i >= destanationGridPosition.y; i--)
        {
            path.Enqueue(new Vector2Int(unitData.gridPosition.x, i));
        }
        for (int i = unitData.gridPosition.y; i <= destanationGridPosition.y; i++)
        {
            path.Enqueue(new Vector2Int(unitData.gridPosition.x, i));
        }

        for (int i = unitData.gridPosition.x; i >= destanationGridPosition.x; i--)
        {
            path.Enqueue(new Vector2Int(i, destanationGridPosition.y));
        }
        for (int i = unitData.gridPosition.x; i <= destanationGridPosition.x; i++)
        {
            path.Enqueue(new Vector2Int(i,  destanationGridPosition.y));
        }

        nextTileGridPosition = path.Dequeue();
        movedOnGrid = false;
        moving = true;
    }

    private void MoveFromTileToTile()
    {
        if (moving && path.Count > 0)
        {
            distanceBetweenTiles += Time.deltaTime * speed;
            unitData.gameObject.transform.position = Vector2.Lerp(HexagonCalculator.GridToHexagonPosition(unitData.gridPosition), HexagonCalculator.GridToHexagonPosition(nextTileGridPosition), distanceBetweenTiles);

            if (distanceBetweenTiles > 0.5 && distanceBetweenTiles < 0.6)
            {
                movedOnGrid = true;
                GameData.units[unitData.gridPosition.x, unitData.gridPosition.y] = null;
                GameData.units[nextTileGridPosition.x, nextTileGridPosition.y] = unitData;
                unitData.gridPosition = nextTileGridPosition;
            }
            else if (distanceBetweenTiles >= 1)
            {
                GameData.units[unitData.gridPosition.x, unitData.gridPosition.y] = null;
                GameData.units[nextTileGridPosition.x, nextTileGridPosition.y] = unitData;
                unitData.gridPosition = nextTileGridPosition;
                unitData.gameObject.transform.position = HexagonCalculator.GridToHexagonPosition(nextTileGridPosition);
                distanceBetweenTiles = 0;
                nextTileGridPosition = path.Dequeue();
                movedOnGrid = false;
                if (path.Count == 0)
                {
                    Debug.Log("Finsihed moving");
                    moving = false;
                }
            }
        }
    }
}
