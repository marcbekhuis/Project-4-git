using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class SetUpGameData : MonoBehaviour
{
    [SerializeField] private PlayerData[] players;
    [SerializeField] private PlayerData thisPlayer;

    [SerializeField] private Tilemap biomeTilemap;
    [SerializeField] private Tilemap buildingTilemap;
    [SerializeField] private Tilemap borderTilemap;

    [SerializeField] private Vector2Int mapSize = new Vector2Int(100,100);
    [SerializeField] private Canvas canvas;

    // Start is called before the first frame update
    void Awake()
    {
        GameData.players = players;
        GameData.thisPlayer = thisPlayer;

        GameData.mapSize = mapSize;
        GameData.biomeTilemap = biomeTilemap;
        GameData.buildingTilemap = buildingTilemap;
        GameData.borderTilemap = borderTilemap;

        GameData.biomes = new GameData.BiomeTypes[mapSize.x, mapSize.y];
        GameData.tiles = new TileData[mapSize.x, mapSize.y];
        GameData.units = new UnitData[mapSize.x, mapSize.y];
        GameData.buildings = new BuildingData[mapSize.x, mapSize.y];

        GameData.canvas = canvas;
    }
}
