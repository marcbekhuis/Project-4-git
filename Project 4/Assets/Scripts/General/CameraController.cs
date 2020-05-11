using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 0.0001f;
    [SerializeField] private float zoomSpeed = 0.02f;
    [SerializeField] private float maxZoomIn = 5;
    [SerializeField] private float maxZoomOut = 20;

    [HideInInspector] static public Camera camera;
    float prefDistance;
    int framesSinceLastZoom = 10;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touches.Length == 1)
        {
            if (Input.touches[0].phase == TouchPhase.Moved)
            {
                camera.transform.position -= (Vector3)Input.touches[0].deltaPosition * movementSpeed * camera.orthographicSize;
            }
        }
        else if (Input.touches.Length == 2)
        {
            if (Input.touches[0].phase == TouchPhase.Moved && Input.touches[1].phase == TouchPhase.Moved)
            {
                float distance = Vector2.Distance(Input.touches[0].position, Input.touches[1].position);
                if (framesSinceLastZoom > 3)
                {
                    prefDistance = distance;
                    framesSinceLastZoom = 0;
                }
                camera.orthographicSize = Mathf.Clamp(camera.orthographicSize - (distance - prefDistance) * zoomSpeed, maxZoomIn, maxZoomOut);
                prefDistance = distance;
            }
        }
        else
        {
            framesSinceLastZoom++;
        }
    }
}
