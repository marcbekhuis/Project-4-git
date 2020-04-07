using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TestPlaceTile : MonoBehaviour
{
    public Tilemap tilemap;
    public Tile tile;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                Debug.LogError("Touch");
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(touch.position);
                //Vector3Int gridPosition = tilemap.WorldToCell(worldPosition);
                tilemap.SetTile((Vector3Int)HexagonCalculator.WorldToHexagonPosition(worldPosition), tile);
            }
        }
    }
}
