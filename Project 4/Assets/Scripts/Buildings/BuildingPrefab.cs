using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New Building", menuName = "ScriptableObjects/BuildingData")]
public class BuildingPrefab : ScriptableObject
{
    public TileBase tile;
    public GameObject actionPanel;
    public float maxHealth = 100;
    [Space]
    public bool townCenter = false;
    [Space]
    public bool producesResources = false;
    public Item itemToProduce;
    public int amount = 1;
    public int productionDelay = 60;
    [Space]
    public int maxNumberOfResidence = 0;
    [Space]
    public bool mustBePlaceInClaim = true;
    public bool destroysUnit = false;
}
