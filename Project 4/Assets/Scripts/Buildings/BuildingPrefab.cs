using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Building", menuName = "ScriptableObjects/BuildingData")]
public class BuildingPrefab : ScriptableObject
{
    public BuildingAction buildingAction;
}
