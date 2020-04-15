using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TownCenter : MonoBehaviour
{
    public Vector2Int gridPosition;
    public TileBase borders;

    [SerializeField] private float claimDelaySec = 120;
    float claimCooldownSec = 0;
    CityData cityData = new CityData();

    private void Start()
    {
        cityData.originLocation = gridPosition;
        FindObjectOfType<PlayerData>().cities.Add(cityData);

        claimCooldownSec = Time.time + claimDelaySec;
        cityData.takenTiles.Add(gridPosition);
        UIElements.bordersTilemap.SetTile((Vector3Int)gridPosition, borders);

        cityData.takenTiles.Add(gridPosition + new Vector2Int(0, 1));
        UIElements.bordersTilemap.SetTile((Vector3Int)gridPosition + new Vector3Int(0, 1, 0), borders);

        cityData.takenTiles.Add(gridPosition + new Vector2Int(1, 0));
        UIElements.bordersTilemap.SetTile((Vector3Int)gridPosition + new Vector3Int(1, 0, 0), borders);

        cityData.takenTiles.Add(gridPosition - new Vector2Int(0, 1));
        UIElements.bordersTilemap.SetTile((Vector3Int)gridPosition - new Vector3Int(0, 1, 0), borders);

        cityData.takenTiles.Add(gridPosition - new Vector2Int(1, 0));
        UIElements.bordersTilemap.SetTile((Vector3Int)gridPosition - new Vector3Int(1, 0, 0), borders);

        if (gridPosition.y % 2 == 1)
        {
            cityData.takenTiles.Add(gridPosition + new Vector2Int(1, 1));
            UIElements.bordersTilemap.SetTile((Vector3Int)gridPosition + new Vector3Int(1, 1, 0), borders);

            cityData.takenTiles.Add(gridPosition + new Vector2Int(1, -1));
            UIElements.bordersTilemap.SetTile((Vector3Int)gridPosition + new Vector3Int(1, -1, 0), borders);
        }
        else
        {
            cityData.takenTiles.Add(gridPosition + new Vector2Int(-1, 1));
            UIElements.bordersTilemap.SetTile((Vector3Int)gridPosition + new Vector3Int(-1, 1, 0), borders);

            cityData.takenTiles.Add(gridPosition + new Vector2Int(-1, -1));
            UIElements.bordersTilemap.SetTile((Vector3Int)gridPosition + new Vector3Int(-1, -1, 0), borders);
        }

    }

    private void Update()
    {
        if (Time.time > claimCooldownSec)
        {
            ClaimTile();
        }
    }

    private void ClaimTile()
    {
        for (int i = 0; i < 100; i++)
        {
            int takenTile = Random.Range(1, cityData.takenTiles.Count);
            Vector2Int nextTilePosition;
            if (cityData.takenTiles[takenTile].y % 2 == 1)
            {
                nextTilePosition = new Vector2Int((int)cityData.takenTiles[takenTile].x + Random.Range(0, 2), (int)cityData.takenTiles[takenTile].y + Random.Range(-1, 2));
            }
            else
            {
                nextTilePosition = new Vector2Int((int)cityData.takenTiles[takenTile].x + Random.Range(-1, 1), (int)cityData.takenTiles[takenTile].y + Random.Range(-1, 2));
            }
            bool alreadyClaimed = false;
            foreach (var tile in cityData.takenTiles)
            {
                if (tile == nextTilePosition)
                {
                    alreadyClaimed = true;
                    break;
                }
            }
            if (!alreadyClaimed)
            {
                cityData.takenTiles.Add(nextTilePosition);
                UIElements.bordersTilemap.SetTile((Vector3Int)nextTilePosition, borders);
                claimCooldownSec = Time.time + claimDelaySec;
                return;
            }
        }
    }
}
