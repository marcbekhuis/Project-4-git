using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TownCenter : MonoBehaviour
{
    [SerializeField] private float claimDelaySec = 120;
    float claimCooldownSec = 0;
    public CityData cityData = new CityData();
    public BuildingData buildingData;

    private void Start()
    {
        cityData.originLocation = buildingData.gridPosition;
        buildingData.ownedByPlayer.cities.Add(cityData);
        buildingData.ownedByCity = cityData;

        claimCooldownSec = Time.time + claimDelaySec;

        // claims the tile the city is in and the surrounding tiles.
        ClaimTile(buildingData.gridPosition);

        ClaimTile(buildingData.gridPosition + new Vector2Int(0, 1));

        ClaimTile(buildingData.gridPosition + new Vector2Int(1, 0));

        ClaimTile(buildingData.gridPosition - new Vector2Int(0, 1));

        ClaimTile(buildingData.gridPosition - new Vector2Int(1, 0));

        if (buildingData.gridPosition.y % 2 == 1)
        {
            ClaimTile(buildingData.gridPosition + new Vector2Int(1, 1));

            ClaimTile(buildingData.gridPosition + new Vector2Int(1, -1));
        }
        else
        {
            ClaimTile(buildingData.gridPosition + new Vector2Int(-1, 1));

            ClaimTile(buildingData.gridPosition + new Vector2Int(-1, -1));
        }

        GameData.fogOfWar.UpdateVisibility();
    }

    private void ClaimTile(Vector2Int position) // sets all the data to claim a tile.
    {
        cityData.takenTiles.Add(position);
        GameData.borderTilemap.SetTile((Vector3Int)position, buildingData.ownedByPlayer.border);
        GameData.tiles[position.x , position.y].ownedByPlayer = buildingData.ownedByPlayer;
        GameData.tiles[position.x, position.y].ownedByCity = cityData;
    }

    private void Update()
    {
        if (Time.time > claimCooldownSec)
        {
            ClaimTile();
        }
    }

    private void ClaimTile() // Finds a unclaimed tile next to a claimed tile and claims it.
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
            if (GameData.tiles[nextTilePosition.x, nextTilePosition.y].ownedByCity == null)
            {
                ClaimTile(nextTilePosition);

                claimCooldownSec = Time.time + claimDelaySec;
                return;
            }
        }
    }
}
