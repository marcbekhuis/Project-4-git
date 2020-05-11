using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "New Unit", menuName = "ScriptableObjects/UnitData")]
public class UnitPrefab : ScriptableObject
{
    public Sprite sprite;
    public float movementSpeedSec = 10;
    public GameObject actionPanel;
    public float maxHealth = 100;
    public float damage = 10;
    public float attackDelaySec = 10;
}
