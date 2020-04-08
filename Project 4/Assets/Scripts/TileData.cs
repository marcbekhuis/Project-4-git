using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileData
{
    public TileData(Vector2 Position, TilePrefab tile)
    {
        position = Position;
        this.tile = tile;
    }

    public TilePrefab tile;
    public Vector2 position;
}
