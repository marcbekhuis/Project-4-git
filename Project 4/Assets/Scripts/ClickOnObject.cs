using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickOnObject : MonoBehaviour
{
    [SerializeField] private GameObject highlight;

    // Update is called once per frame
    void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            if (Input.touches.Length == 1)
            {
                if(touch.phase == TouchPhase.Began)
                {
                    Vector2Int gridPosition = HexagonCalculator.HexagonToGridPosition(CameraController.camera.ScreenToWorldPoint(touch.position));
                    Vector3 worldPosition = HexagonCalculator.GridToHexagonPosition(gridPosition);
                    highlight.transform.position = new Vector3(worldPosition.x, worldPosition.y, 0);

                    if (SelectUnitMoveToPos.moveUnit) return;

                    if (GameData.units[gridPosition.x, gridPosition.y] != null)
                    {
                        if (GameData.units[gridPosition.x, gridPosition.y].ownedByPlayer == GameData.thisPlayer)
                        {
                            GameData.units[gridPosition.x, gridPosition.y].OpenActionPanel();
                        }
                    }
                    else if (GameData.buildings[gridPosition.x, gridPosition.y] != null)
                    {
                        if (GameData.units[gridPosition.x, gridPosition.y].ownedByPlayer == GameData.thisPlayer)
                        {
                            GameData.buildings[gridPosition.x, gridPosition.y].OpenActionPanel();
                        }
                    }
                }
            }
        }
    }
}
