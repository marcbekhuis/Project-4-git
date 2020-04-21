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
    [SerializeField] private Tilemap undiscoveredFogTilemap;
    [SerializeField] private Tilemap hidenFogTilemap;
    [SerializeField] private FogOfWar fogOfWar;

    [SerializeField] private Vector2Int mapSize = new Vector2Int(100,100);
    [SerializeField] private Canvas canvas;

    // Start is called before the first frame update
    void Awake()
    {
        GameData.players = players;
        GameData.thisPlayer = thisPlayer;
        thisPlayer.name = LobbyData.playerName;

        GameData.mapSize = mapSize;
        GameData.biomeTilemap = biomeTilemap;
        GameData.buildingTilemap = buildingTilemap;
        GameData.borderTilemap = borderTilemap;
        GameData.undiscoveredFogTilemap = undiscoveredFogTilemap;
        GameData.hidenFogTilemap = hidenFogTilemap;
        GameData.fogOfWar = fogOfWar;

        GameData.biomes = new GameData.BiomeTypes[mapSize.x, mapSize.y];
        GameData.tiles = new TileData[mapSize.x, mapSize.y];
        GameData.units = new UnitData[mapSize.x, mapSize.y];
        GameData.buildings = new BuildingData[mapSize.x, mapSize.y];

        foreach (var player in players)
        {
            player.tileVisibility = new GameData.TileVisibility[mapSize.x, mapSize.y];
        }

        GameData.canvas = canvas;
    }
}
