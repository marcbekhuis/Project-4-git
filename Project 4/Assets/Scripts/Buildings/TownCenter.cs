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
        cityData.takenTiles.Add(buildingData.gridPosition);
        GameData.borderTilemap.SetTile((Vector3Int)buildingData.gridPosition, buildingData.ownedByPlayer.border);
        GameData.tiles[buildingData.gridPosition.x, buildingData.gridPosition.y].ownedByPlayer = buildingData.ownedByPlayer;
        GameData.tiles[buildingData.gridPosition.x, buildingData.gridPosition.y].ownedByCity = cityData;

        cityData.takenTiles.Add(buildingData.gridPosition + new Vector2Int(0, 1));
        GameData.borderTilemap.SetTile((Vector3Int)buildingData.gridPosition + new Vector3Int(0, 1, 0), buildingData.ownedByPlayer.border);
        GameData.tiles[buildingData.gridPosition.x, buildingData.gridPosition.y + 1].ownedByPlayer = buildingData.ownedByPlayer;
        GameData.tiles[buildingData.gridPosition.x, buildingData.gridPosition.y + 1].ownedByCity = cityData;

        cityData.takenTiles.Add(buildingData.gridPosition + new Vector2Int(1, 0));
        GameData.borderTilemap.SetTile((Vector3Int)buildingData.gridPosition + new Vector3Int(1, 0, 0), buildingData.ownedByPlayer.border);
        GameData.tiles[buildingData.gridPosition.x + 1, buildingData.gridPosition.y].ownedByPlayer = buildingData.ownedByPlayer;
        GameData.tiles[buildingData.gridPosition.x + 1, buildingData.gridPosition.y].ownedByCity = cityData;

        cityData.takenTiles.Add(buildingData.gridPosition - new Vector2Int(0, 1));
        GameData.borderTilemap.SetTile((Vector3Int)buildingData.gridPosition - new Vector3Int(0, 1, 0), buildingData.ownedByPlayer.border);
        GameData.tiles[buildingData.gridPosition.x, buildingData.gridPosition.y - 1].ownedByPlayer = buildingData.ownedByPlayer;
        GameData.tiles[buildingData.gridPosition.x, buildingData.gridPosition.y - 1].ownedByCity = cityData;

        cityData.takenTiles.Add(buildingData.gridPosition - new Vector2Int(1, 0));
        GameData.borderTilemap.SetTile((Vector3Int)buildingData.gridPosition - new Vector3Int(1, 0, 0), buildingData.ownedByPlayer.border);
        GameData.tiles[buildingData.gridPosition.x - 1, buildingData.gridPosition.y].ownedByPlayer = buildingData.ownedByPlayer;
        GameData.tiles[buildingData.gridPosition.x - 1, buildingData.gridPosition.y].ownedByCity = cityData;

        if (buildingData.gridPosition.y % 2 == 1)
        {
            cityData.takenTiles.Add(buildingData.gridPosition + new Vector2Int(1, 1));
            GameData.borderTilemap.SetTile((Vector3Int)buildingData.gridPosition + new Vector3Int(1, 1, 0), buildingData.ownedByPlayer.border);
            GameData.tiles[buildingData.gridPosition.x + 1, buildingData.gridPosition.y + 1].ownedByPlayer = buildingData.ownedByPlayer;
            GameData.tiles[buildingData.gridPosition.x + 1, buildingData.gridPosition.y + 1].ownedByCity = cityData;

            cityData.takenTiles.Add(buildingData.gridPosition + new Vector2Int(1, -1));
            GameData.borderTilemap.SetTile((Vector3Int)buildingData.gridPosition + new Vector3Int(1, -1, 0), buildingData.ownedByPlayer.border);
            GameData.tiles[buildingData.gridPosition.x + 1, buildingData.gridPosition.y - 1].ownedByPlayer = buildingData.ownedByPlayer;
            GameData.tiles[buildingData.gridPosition.x + 1, buildingData.gridPosition.y - 1].ownedByCity = cityData;
        }
        else
        {
            cityData.takenTiles.Add(buildingData.gridPosition + new Vector2Int(-1, 1));
            GameData.borderTilemap.SetTile((Vector3Int)buildingData.gridPosition + new Vector3Int(-1, 1, 0), buildingData.ownedByPlayer.border);
            GameData.tiles[buildingData.gridPosition.x - 1, buildingData.gridPosition.y + 1].ownedByPlayer = buildingData.ownedByPlayer;
            GameData.tiles[buildingData.gridPosition.x - 1, buildingData.gridPosition.y + 1].ownedByCity = cityData;

            cityData.takenTiles.Add(buildingData.gridPosition + new Vector2Int(-1, -1));
            GameData.borderTilemap.SetTile((Vector3Int)buildingData.gridPosition + new Vector3Int(-1, -1, 0), buildingData.ownedByPlayer.border);
            GameData.tiles[buildingData.gridPosition.x - 1, buildingData.gridPosition.y - 1].ownedByPlayer = buildingData.ownedByPlayer;
            GameData.tiles[buildingData.gridPosition.x - 1, buildingData.gridPosition.y - 1].ownedByCity = cityData;
        }

        GameData.fogOfWar.UpdateVisibility();
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
            if (GameData.tiles[nextTilePosition.x, nextTilePosition.y].ownedByCity == null)
            {
                cityData.takenTiles.Add(nextTilePosition);
                GameData.borderTilemap.SetTile((Vector3Int)nextTilePosition, GameData.thisPlayer.border);
                GameData.tiles[nextTilePosition.x, nextTilePosition.y].ownedByPlayer = buildingData.ownedByPlayer;
                claimCooldownSec = Time.time + claimDelaySec;
                return;
            }
        }
    }
}
