using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementKeys : MonoBehaviour
{
    public KeyCode up;
    public KeyCode down;
    public KeyCode left;
    public KeyCode right;

    public void SetKeys(KeyCode upKey, KeyCode downKey, KeyCode leftKey, KeyCode rightKey)
    {
        up = upKey;
        down = downKey;
        left = leftKey;
        right = rightKey;
    }
}
