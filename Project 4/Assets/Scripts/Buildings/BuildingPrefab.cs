using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New Building", menuName = "ScriptableObjects/BuildingData")]
public class BuildingPrefab : ScriptableObject
{
    public TileBase tile;
    public GameObject buildingScripts;
    public GameObject actionPanel;
    public bool mustBePlaceInClaim = true;
    public bool destroysUnit = false;
}
