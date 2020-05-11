using System.Collections;
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
                    if (touch.phase == TouchPhase.Began) // Gets a move to position for the selected unit.
                    {
                        Vector2Int gridPosition = HexagonCalculator.HexagonToGridPosition(CameraController.camera.ScreenToWorldPoint(touch.position));

                        unitsData.unitMovement.SetDestanationGrid(gridPosition);
                        moveUnit = false;

                        Destroy(GameData.activeActionPanel);
                        GameData.selectedUnit = null;
                    }
                }
            }
        }
    }
}
