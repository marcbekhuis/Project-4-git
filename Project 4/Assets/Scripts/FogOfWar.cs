using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FogOfWar : MonoBehaviour
{
    [SerializeField] private TileBase undiscoveredFog;
    [SerializeField] private TileBase hidenFog;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void UpdateVisibility()
    {
        for (int x = 0; x < GameData.mapSize.x; x++)
        {
            for (int y = 0; y < GameData.mapSize.x; y++)
            {
                if (GameData.thisPlayer.tileVisibility[x, y] == GameData.TileVisibility.Visible)
                {
                    GameData.thisPlayer.tileVisibility[x, y] = GameData.TileVisibility.Hiden;
                }
            }
        }

        foreach (var unit in GameData.thisPlayer.units)
        {
            Vector2Int position = unit.gridPosition;
            GameData.thisPlayer.tileVisibility[position.x, position.y] = GameData.TileVisibility.Visible;

            position = unit.gridPosition + new Vector2Int(0, 1);
            GameData.thisPlayer.tileVisibility[position.x, position.y] = GameData.TileVisibility.Visible;

            position = unit.gridPosition + new Vector2Int(1, 0);
            GameData.thisPlayer.tileVisibility[position.x, position.y] = GameData.TileVisibility.Visible;

            position = unit.gridPosition - new Vector2Int(0, 1);
            GameData.thisPlayer.tileVisibility[position.x, position.y] = GameData.TileVisibility.Visible;

            position = unit.gridPosition - new Vector2Int(1, 0);
            GameData.thisPlayer.tileVisibility[position.x, position.y] = GameData.TileVisibility.Visible;

            if (unit.gridPosition.y % 2 == 1)
            {
                position = unit.gridPosition + new Vector2Int(1, 1);
                GameData.thisPlayer.tileVisibility[position.x, position.y] = GameData.TileVisibility.Visible;

                position = unit.gridPosition + new Vector2Int(1, -1);
                GameData.thisPlayer.tileVisibility[position.x, position.y] = GameData.TileVisibility.Visible;
            }
            else
            {
                position = unit.gridPosition + new Vector2Int(-1, 1);
                GameData.thisPlayer.tileVisibility[position.x, position.y] = GameData.TileVisibility.Visible;

                position = unit.gridPosition + new Vector2Int(-1, -1);
                GameData.thisPlayer.tileVisibility[position.x, position.y] = GameData.TileVisibility.Visible;
            }

        }

        UpdateFogMap();
    }


    private void UpdateFogMap()
    {
        for (int x = 0; x < GameData.mapSize.x; x++)
        {
            for (int y = 0; y < GameData.mapSize.x; y++)
            {
                if (GameData.thisPlayer.tileVisibility[x, y] == GameData.TileVisibility.Undiscovered)
                {
                    if (GameData.undiscoveredFogTilemap.GetTile(new Vector3Int(x, y, 0)) != undiscoveredFog)
                    {
                        GameData.undiscoveredFogTilemap.SetTile(new Vector3Int(x, y, 0), undiscoveredFog);
                    }
                }
                else if (GameData.thisPlayer.tileVisibility[x, y] == GameData.TileVisibility.Hiden)
                {
                    if (GameData.hidenFogTilemap.GetTile(new Vector3Int(x, y, 0)) != hidenFog)
                    {
                        GameData.hidenFogTilemap.SetTile(new Vector3Int(x, y, 0), hidenFog);
                    }

                    if (GameData.undiscoveredFogTilemap.GetTile(new Vector3Int(x, y, 0)) != null)
                    {
                        GameData.undiscoveredFogTilemap.SetTile(new Vector3Int(x, y, 0), null);
                    }
                }
                else if (GameData.thisPlayer.tileVisibility[x, y] == GameData.TileVisibility.Visible)
                {
                    if (GameData.hidenFogTilemap.GetTile(new Vector3Int(x, y, 0)) != null)
                    {
                        GameData.hidenFogTilemap.SetTile(new Vector3Int(x, y, 0), null);
                    }

                    if (GameData.undiscoveredFogTilemap.GetTile(new Vector3Int(x, y, 0)) != null)
                    {
                        GameData.undiscoveredFogTilemap.SetTile(new Vector3Int(x, y, 0), null);
                    }
                }
            }
        }
    }
}
