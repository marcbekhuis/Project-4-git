using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Camera camera;
    Vector3 prefPosition;
    Vector2 prefPositionCamera;
    bool firstTime = true;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            if (Input.touches.Length == 1)
            {
                if (touch.phase == TouchPhase.Moved)
                {
                    if (Vector2.Distance(touch.position, prefPositionCamera) > 0.1f)
                    {
                        prefPositionCamera = touch.position;
                        Vector3 position = camera.ScreenToWorldPoint(touch.position);
                        if (firstTime)
                        {
                            prefPosition = position;
                            firstTime = false;
                        }

                        Debug.LogError(position - prefPosition);
                        camera.transform.position += prefPosition - position;
                        prefPosition = position;
                    }
                }
            }
        }
    }
}
