using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectUnitMoveToPos : MonoBehaviour
{
    public static bool moveUnit = false;
    public UnitData unitsData;

    BasicMovement basicMovement;
    Vector2Int position;

    private void Start()
    {
        moveUnit = false;
    }

    public void ActivateMove()
    {
        moveUnit = true;
        if (UIElements.selectedObject != null)
        {
            basicMovement = UIElements.selectedObject.gameObject.GetComponent<BasicMovement>();
            basicMovement.destination = position;
        }
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
                        position = HexagonCalculator.HexagonToWorldPosition(CameraController.camera.ScreenToWorldPoint(touch.position));

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
