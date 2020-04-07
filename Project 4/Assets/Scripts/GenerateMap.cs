using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GenerateMap : MonoBehaviour
{
    public Tilemap tilemap;
    public TilePrefab landTile;
    public TilePrefab waterTile;
    public TilePrefab desertTile;
    public TilePrefab jungleTile;
    public TilePrefab mountainTile;
    public int directionTryLimit = 100;
    public Vector2Int mapSize;
    [Header("Water")]
    public Vector2Int waterSize;
    public Vector2 waterNumberPer100X100;
    [Header("Ocean")]
    public Vector2Int oceanSize;
    public Vector2 oceanNumberPer100X100;
    [Header("Desert")]
    public Vector2Int desertSize;
    public Vector2 desertNumberPer100X100;
    [Header("Jungle")]
    public Vector2Int jungleSize;
    public Vector2 jungleNumberPer100X100;
    [Header("Mountain")]
    public Vector2Int mountainSize;
    public Vector2 mountainNumberPer100X100;

    private TileTypes[,] tileTypes;

    enum TileTypes
    {
        Land,
        Water,
        Jungle,
        Desert,
        Mountain
    }

    // Start is called before the first frame update
    void Start()
    {
        Generate();
    }

    private void Generate()
    {
        tileTypes = new TileTypes[mapSize.x, mapSize.y];
        GenerateBiomes(desertNumberPer100X100, desertSize, TileTypes.Desert);
        GenerateBiomes(jungleNumberPer100X100, jungleSize, TileTypes.Jungle);
        GenerateBiomes(mountainNumberPer100X100, mountainSize, TileTypes.Mountain);
        GenerateBiomes(waterNumberPer100X100, waterSize, TileTypes.Water);
        GenerateBiomes(oceanNumberPer100X100, oceanSize, TileTypes.Water);
        PlaceTiles();
    }

    private void PlaceTiles()
    {
        Tiles.tiles = new TileData[mapSize.x,mapSize.y];
        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                switch (tileTypes[x,y])
                {
                    case TileTypes.Land:
                        tilemap.SetTile((Vector3Int)HexagonCalculator.WorldToHexagonPosition(new Vector2(x, y)), landTile.tile);
                        Tiles.tiles[x, y] = new TileData(HexagonCalculator.WorldToHexagonPosition(new Vector2(x, y)), landTile);
                        break;
                    case TileTypes.Water:
                        tilemap.SetTile((Vector3Int)HexagonCalculator.WorldToHexagonPosition(new Vector2(x, y)), waterTile.tile);
                        Tiles.tiles[x, y] = new TileData(HexagonCalculator.WorldToHexagonPosition(new Vector2(x, y)), waterTile);
                        break;
                    case TileTypes.Desert:
                        tilemap.SetTile((Vector3Int)HexagonCalculator.WorldToHexagonPosition(new Vector2(x, y)), desertTile.tile);
                        Tiles.tiles[x, y] = new TileData(HexagonCalculator.WorldToHexagonPosition(new Vector2(x, y)), desertTile);
                        break;
                    case TileTypes.Jungle:
                        tilemap.SetTile((Vector3Int)HexagonCalculator.WorldToHexagonPosition(new Vector2(x, y)), jungleTile.tile);
                        Tiles.tiles[x, y] = new TileData(HexagonCalculator.WorldToHexagonPosition(new Vector2(x, y)), jungleTile);
                        break;
                    case TileTypes.Mountain:
                        tilemap.SetTile((Vector3Int)HexagonCalculator.WorldToHexagonPosition(new Vector2(x, y)), mountainTile.tile);
                        Tiles.tiles[x, y] = new TileData(HexagonCalculator.WorldToHexagonPosition(new Vector2(x, y)), mountainTile);
                        break;
                    default:
                        break;
                }
            }
        }
    }

    private void GenerateBiomes(Vector2 biomeNumberPer10X10, Vector2 biomeSize, TileTypes tileType)
    {
        int numberOfWaters = Mathf.RoundToInt(Random.Range(biomeNumberPer10X10.x * mapSize.x / 100 * mapSize.y / 100, biomeNumberPer10X10.y * mapSize.x / 100 * mapSize.y / 100));

        for (int i = 0; i < numberOfWaters; i++)
        {
            GenerateBiome((int)Random.Range(biomeSize.x, biomeSize.y), tileType);
        }
    }

    private void GenerateBiome(int biomeSize, TileTypes tileType)
    {
        Vector2Int beginTileLocation = new Vector2Int(Random.Range(0, mapSize.x), Random.Range(0, mapSize.y));
        Vector2Int biomeTileLocation = beginTileLocation;

        for (int i = 0; i < biomeSize; i++)
        {
            tileTypes[biomeTileLocation.x, biomeTileLocation.y] = tileType;
            biomeTileLocation = GetNewPosition(biomeTileLocation, beginTileLocation, biomeSize, tileType);
        }
    }

    private Vector2Int GetNewPosition(Vector2Int TileLocation, Vector2Int beginLocation, int biomeSize, TileTypes tileType)
    {
        int prefDirection = 4;
        for (int i = 0; i < directionTryLimit; i++)
        {
            int direction = Random.Range(0, 4);
            if (i % 50f == 0 & i != 0)
            {
                int range = Mathf.Clamp(biomeSize / 50, 5, 50);
                TileLocation = new Vector2Int(Mathf.Clamp(beginLocation.x + Random.Range(-range, range + 1), 0, mapSize.x - 1), Mathf.Clamp(beginLocation.y + Random.Range(-range, range + 1), 0, mapSize.y - 1));
            }
            else if (prefDirection != 4)
            {
                if (direction == prefDirection)
                {
                    if (direction == 3)
                    {
                        direction = 0;
                    }
                    else
                    {
                        direction++;
                    }
                }
            }
            switch (direction)
            {
                case 0:
                    if (TileLocation.x > 0)
                    {
                        if (tileTypes[TileLocation.x - 1, TileLocation.y] != tileType)
                        {
                            return TileLocation - new Vector2Int(1, 0);
                        }
                    }
                    prefDirection = 0;
                    break;
                case 1:
                    if (TileLocation.y > 0)
                    {
                        if (tileTypes[TileLocation.x, TileLocation.y - 1] != tileType)
                        {
                            return TileLocation - new Vector2Int(0, 1);
                        }
                    }
                    prefDirection = 1;
                    break;
                case 2:
                    if (TileLocation.x < mapSize.x - 1)
                    {
                        if (tileTypes[TileLocation.x + 1, TileLocation.y] != tileType)
                        {
                            return TileLocation + new Vector2Int(1, 0);
                        }
                    }
                    prefDirection = 2;
                    break;
                case 3:
                    if (TileLocation.y < mapSize.y - 1)
                    {
                        if (tileTypes[TileLocation.x, TileLocation.y + 1] != tileType)
                        {
                            return TileLocation + new Vector2Int(0, 1);
                        }
                    }
                    prefDirection = 3;
                    break;
            }
        }
        return TileLocation;
    }
}
