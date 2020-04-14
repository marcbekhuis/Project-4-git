using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownCenter : MonoBehaviour
{
    public Vector2Int gridPosition;

    float claimDelaySec = 120;
    float claimCooldownSec = 0;
    CityData cityData = new CityData();

    private void Start()
    {
        claimCooldownSec = Time.time + claimDelaySec;
        cityData.takenTiles.Add(gridPosition);
        cityData.takenTiles.Add(gridPosition + new Vector2Int(0, 1));
        cityData.takenTiles.Add(gridPosition + new Vector2Int(1, 0));
        cityData.takenTiles.Add(gridPosition - new Vector2Int(0, 1));
        cityData.takenTiles.Add(gridPosition - new Vector2Int(1, 0));
        cityData.takenTiles.Add(gridPosition + new Vector2Int(1, 1));
        cityData.takenTiles.Add(gridPosition + new Vector2Int(1, -1));
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
            Vector2 nextTilePosition = cityData.takenTiles[takenTile] + new Vector2Int(Random.Range(-1,2), Random.Range(-1, 2));
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
            }
        }
    }
}
