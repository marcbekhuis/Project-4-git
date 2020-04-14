using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickOnObject : MonoBehaviour
{
    [SerializeField] private GameObject highlight;

    // Update is called once per frame
    void Update()
    {
        if (SelectUnitMoveToPos.moveUnit) return;

        foreach (Touch touch in Input.touches)
        {
            if (Input.touches.Length == 1)
            {
                if(touch.phase == TouchPhase.Began)
                {
                    Vector2Int gridPosition = HexagonCalculator.HexagonToWorldPosition(CameraController.camera.ScreenToWorldPoint(touch.position));
                    Vector3 worldPosition = HexagonCalculator.WorldToHexagonPosition(gridPosition);

                    if (Units.units[gridPosition.x, gridPosition.y] != null)
                    {
                        Units.units[gridPosition.x, gridPosition.y].OpenActionPanel();
                    }
                    else if (Buildings.buildings[gridPosition.x, gridPosition.y] != null)
                    {
                        Buildings.buildings[gridPosition.x, gridPosition.y].OpenActionPanel();
                    }

                    highlight.transform.position = new Vector3(worldPosition.x, worldPosition.y, 0);
                }
            }
        }
    }
}
