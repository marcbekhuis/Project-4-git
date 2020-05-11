using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public static class GameData
{
    public static PlayerData[] players;
    public static PlayerData thisPlayer;

    public static Tilemap biomeTilemap;
    public static Tilemap buildingTilemap;
    public static Tilemap borderTilemap;
    public static Tilemap undiscoveredFogTilemap;
    public static Tilemap hidenFogTilemap;

    public static Vector2Int mapSize;
    public static BiomeTypes[,] biomes;
    public static TileData[,] tiles;
    public static UnitData[,] units;
    public static BuildingData[,] buildings;
    public static FogOfWar fogOfWar;

    public static GameObject activeActionPanel;
    public static BuildingData selectedBuilding;
    public static UnitData selectedUnit;
    public static Canvas canvas;
    public static GameObject gameOverScreen;

    public enum BiomeTypes
    {
        Plains,
        Water,
        Ocean,
        Jungle,
        Desert,
        Mountain,
        Forest
    }

    public enum TileVisibility
    {
        Undiscovered,
        Visible,
        Hiden,
    }
}
