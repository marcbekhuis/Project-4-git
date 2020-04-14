﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectUnitMoveToPos : MonoBehaviour
{
    public static bool moveUnit = false;
    public UnitData unitsData;

    private void Start()
    {
        moveUnit = false;
    }

    public void ActivateMove()
    {
        //Sets a new destination of the unit once the move button is clicked
        moveUnit = true;
    }

    private void Update()
    {
        if (moveUnit)
        {
            foreach (Touch touch in Input.touches)
            {
                if (Input.touches.Length == 1)
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        Vector2Int position = HexagonCalculator.HexagonToWorldPosition(CameraController.camera.ScreenToWorldPoint(touch.position));

                        Units.units[unitsData.positionGrid.x, unitsData.positionGrid.y] = null;
                        Units.units[position.x, position.y] = unitsData;
                        unitsData.positionGrid = position;

                        Vector3 worldPosition = HexagonCalculator.WorldToHexagonPosition(position);

                        unitsData.basicMovement.destination = worldPosition;
                        moveUnit = false;

                        Destroy(UIElements.activeUnitPanel);
                        UIElements.selectedObject = null;
                    }
                }
            }
        }
    }
}
