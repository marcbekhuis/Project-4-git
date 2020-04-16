using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileData
{
    public TileData(Vector2Int GridPosition, TilePrefab tile, PlayerData OwnedByPlayer)
    {
        gridPoitiom = GridPosition;
        this.tile = tile;
        ownedByPlayer = OwnedByPlayer;
    }

    public TilePrefab tile;
    public Vector2Int gridPoitiom;
    public PlayerData ownedByPlayer;
}
