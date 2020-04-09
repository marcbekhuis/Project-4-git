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
                    Vector2Int position = HexagonCalculator.HexagonToWorldPosition(CameraController.camera.ScreenToWorldPoint(touch.position));
                    if (Units.units[position.x, position.y] != null)
                    {
                        Units.units[position.x, position.y].OpenActionPanel();
                    }
                    Vector3 worldPosition = HexagonCalculator.WorldToHexagonPosition(position);
                    highlight.transform.position = new Vector3(worldPosition.x, worldPosition.y, 0);
                }
            }
        }
    }
}
