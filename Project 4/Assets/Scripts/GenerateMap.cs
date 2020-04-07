using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GenerateMap : MonoBehaviour
{
    public Tilemap tilemap;
    public Tile testTile;
    // Start is called before the first frame update
    void Start()
    {
        Generate(new Vector2Int(10,10));
    }

    private void Generate(Vector2Int size)
    {
        Tiles.tiles = new TileData[size.x,size.y];
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                tilemap.SetTile((Vector3Int)HexagonCalculator.WorldToHexagonPosition(new Vector2(x,y)), testTile);
            }
        }
    }
}
