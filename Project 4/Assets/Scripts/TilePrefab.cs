using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New Tile", menuName = "ScriptableObjects/TileData")]
public class TilePrefab : ScriptableObject
{
    public TileBase tile;
    public bool allowMovement = true;
}
