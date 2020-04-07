using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HexagonCalculator
{
    public static Vector2Int WorldToHexagonPosition(Vector2 position)
    {
        float col = position.x + (position.y % 1);
        float row = position.y;
        return new Vector2Int(Mathf.RoundToInt(col), Mathf.RoundToInt(row));
    }

    public static Vector2Int HexagonToWorldPosition(Vector2 position)
    {
        float col = position.x - (position.y % 1);
        float row = position.y;
        return new Vector2Int(Mathf.RoundToInt(col), Mathf.RoundToInt(row));
    }
}
