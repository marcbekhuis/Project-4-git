using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HexagonCalculator
{
    public static Vector2Int GetHexagonPosition(Vector2 position)
    {
        float col = position.x + (position.y - (position.y % 1)) / 2;
        float row = position.y;
        return new Vector2Int(Mathf.RoundToInt(col), Mathf.RoundToInt(row));
    }
}
