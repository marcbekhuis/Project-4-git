using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickOnUnit : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            if (Input.touches.Length == 1)
            {
                if(touch.phase == TouchPhase.Began)
                {
                    Vector2Int position = HexagonCalculator.HexagonToWorldPosition(CameraController.camera.ScreenToWorldPoint(touch.position));
                    if (Units.units[position.x, position.y] != null)
                    {
                        Units.units[position.x, position.y].OpenActionPanel();
                    }
                }
            }
        }
    }
}
