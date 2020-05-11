using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickOnObject : MonoBehaviour
{
    [SerializeField] private GameObject highlight;
    private EventSystem eventSystem;

    private void Awake()
    {
        eventSystem = GetComponent<EventSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            if (Input.touches.Length == 1)
            {
                if(touch.phase == TouchPhase.Began)
                {
                    if (eventSystem.currentSelectedGameObject != null) return;

                    Vector2Int gridPosition = HexagonCalculator.HexagonToGridPosition(CameraController.camera.ScreenToWorldPoint(touch.position));
                    Vector3 worldPosition = HexagonCalculator.GridToHexagonPosition(gridPosition);
                    highlight.transform.position = new Vector3(worldPosition.x, worldPosition.y, 0);

                    if (SelectUnitMoveToPos.moveUnit) return;
                    if (gridPosition.x < 0) return;
                    if (gridPosition.y < 0) return;
                    if (gridPosition.x > GameData.mapSize.x) return;
                    if (gridPosition.y > GameData.mapSize.y) return;

                    if (GameData.units[gridPosition.x, gridPosition.y] != null)
                    {
                        if (!GameData.units[gridPosition.x, gridPosition.y].unit.actionPanel) return;
                        if (GameData.units[gridPosition.x, gridPosition.y].ownedByPlayer == GameData.thisPlayer)
                        {
                            GameData.units[gridPosition.x, gridPosition.y].OpenActionPanel();
                        }
                    }
                    else if (GameData.buildings[gridPosition.x, gridPosition.y] != null)
                    {
                        if (!GameData.buildings[gridPosition.x, gridPosition.y].building.actionPanel) return;
                        if (GameData.buildings[gridPosition.x, gridPosition.y].ownedByPlayer == GameData.thisPlayer)
                        {
                            GameData.buildings[gridPosition.x, gridPosition.y].OpenActionPanel();
                        }
                    }
                }
            }
        }
    }
}
