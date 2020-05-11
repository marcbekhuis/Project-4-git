using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HexagonCalculator
{
    public static Vector2 GridToHexagonPosition(Vector2 position)
    {
        float col;
        if (position.y % 2 == 1)
        {
            col = position.x + 0.5f;
        }
        else
        {
            col = position.x;
        }
        float row = position.y * 0.75f;
        return new Vector2(col, row);
    }

    public static Vector2Int HexagonToGridPosition(Vector2 position)
    {
        float col;
        if (position.y % 2 == 1)
        {
            col = position.x - 0.5f;
        }
        else
        {
            col = position.x;
        }
        float row = position.y / 0.75f * 1;
        return new Vector2Int(Mathf.RoundToInt(col), Mathf.RoundToInt(row));
    }
}
