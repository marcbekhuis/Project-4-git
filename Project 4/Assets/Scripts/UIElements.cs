using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class UIElements
{
    public static GameObject activeUnitPanel;
    public static GameObject canvas;
    public static GameObject selectedObject;
}

public class SetupUIElements : MonoBehaviour
{
    [SerializeField] private GameObject canves;

    private void Start()
    {
        UIElements.canvas = canves;
    }
}
