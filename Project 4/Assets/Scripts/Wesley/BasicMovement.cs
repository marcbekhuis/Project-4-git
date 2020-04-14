using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    MovementKeys playerKeys = new MovementKeys();
    public Vector2 destination;

    [HideInInspector]public bool isConstructed = false;
    [Tooltip("Makes it so that this object merges into the movePosition object")]public bool doesMerge = true;

    [SerializeField, Range(0, 10f)] float speed = 2.5f;
    float moveX = 0;
    float moveY = 0;
    [SerializeField] bool isPlayer = false;

    private void Start()
    {
        moveX = this.transform.position.x;
        moveY = this.transform.position.y;
        playerKeys.SetKeys(KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D);
        if (destination == Vector2.zero)
        {
            destination = transform.position;
        }
    }

    void Update()
    {
        if (!isConstructed)
        {
            if (isPlayer)
            {
                if (Input.GetKey(playerKeys.up))
                {
                    Movement("Up");
                }
                if (Input.GetKey(playerKeys.down))
                {
                    Movement("Down");
                }
                if (Input.GetKey(playerKeys.left))
                {
                    Movement("Left");
                }
                if (Input.GetKey(playerKeys.right))
                {
                    Movement("Right");
                }
            }
            else
            {
                if (destination != Vector2.zero)
                {
                    DestinationMovement();
                }
            }
        }
    }

    void DestinationMovement()
    {
        Vector2 thisPosition = new Vector2(this.transform.position.x, this.transform.position.y);
        float distance = Vector2.Distance(thisPosition, destination);


        this.transform.position = Vector2.Lerp(thisPosition, destination, speed / 1000);
        moveX = this.transform.position.x;
        moveY = this.transform.position.y;

        if (distance <= 0.05f)
        {
            this.transform.position = destination;
            destination = Vector2.zero;
        }
    }

    public void Movement(string direction)
    {
        if (direction == "Up")
        {
            moveY += speed * Time.deltaTime;
        }
        else if (direction == "Down")
        {
            moveY -= speed * Time.deltaTime;
        }

        if (direction == "Right")
        {
            moveX += speed * Time.deltaTime;
        }
        else if (direction == "Left")
        {
            moveX -= speed * Time.deltaTime;
        }

        this.transform.position = new Vector3(moveX, moveY, this.transform.position.z);
    }
}
