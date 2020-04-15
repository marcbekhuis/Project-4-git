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
        for (int i = unitData.positionGrid.y; i >= destanationGridPosition.y; i--)
        {
            path.Enqueue(new Vector2Int(unitData.positionGrid.x, i));
        }
        for (int i = unitData.positionGrid.y; i <= destanationGridPosition.y; i++)
        {
            path.Enqueue(new Vector2Int(unitData.positionGrid.x, i));
        }

        for (int i = unitData.positionGrid.x; i >= destanationGridPosition.x; i--)
        {
            path.Enqueue(new Vector2Int(i, destanationGridPosition.y));
        }
        for (int i = unitData.positionGrid.x; i <= destanationGridPosition.x; i++)
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
            unitData.gameObject.transform.position = Vector2.Lerp(HexagonCalculator.GridToHexagonPosition(unitData.positionGrid), HexagonCalculator.GridToHexagonPosition(nextTileGridPosition), distanceBetweenTiles);

            if (distanceBetweenTiles > 0.5 && distanceBetweenTiles < 0.6)
            {
                movedOnGrid = true;
                Units.units[unitData.positionGrid.x, unitData.positionGrid.y] = null;
                Units.units[nextTileGridPosition.x, nextTileGridPosition.y] = unitData;
                unitData.positionGrid = nextTileGridPosition;
            }
            else if (distanceBetweenTiles >= 1)
            {
                Units.units[unitData.positionGrid.x, unitData.positionGrid.y] = null;
                Units.units[nextTileGridPosition.x, nextTileGridPosition.y] = unitData;
                unitData.positionGrid = nextTileGridPosition;
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
