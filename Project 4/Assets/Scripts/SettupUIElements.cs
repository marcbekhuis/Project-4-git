using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SettupUIElements : MonoBehaviour
{
    [SerializeField] private GameObject canves;
    [SerializeField] private Tilemap buildingsTilemap;

    private void Awake()
    {
        UIElements.canvas = canves;
        UIElements.buildingsTilemap = buildingsTilemap;
    }
}
