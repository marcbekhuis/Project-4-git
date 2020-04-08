using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettupUIElements : MonoBehaviour
{
    [SerializeField] private GameObject canves;

    private void Start()
    {
        UIElements.canvas = canves;
    }
}
