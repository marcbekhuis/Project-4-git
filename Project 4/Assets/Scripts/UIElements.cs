using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public static class UIElements
{
    public static GameObject activeUnitPanel;
    public static GameObject canvas;
    public static GameObject selectedObject;
    public static Tilemap buildingsTilemap;
}

public class SetupUIElements : MonoBehaviour
{
    [SerializeField] private GameObject canves;

    private void Start()
    {
        UIElements.canvas = canves;
    }
}
