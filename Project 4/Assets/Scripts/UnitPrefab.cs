using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Unit", menuName = "ScriptableObjects/UnitData")]
public class UnitPrefab : ScriptableObject
{
    public Sprite sprite;
    public float movementSpeedSec = 10;
    public GameObject actionPanel;
}
