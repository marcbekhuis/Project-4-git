using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    enum KeyState
    {
        KeyDown,
        Key,
        KeyUp
    }


    [System.Serializable]
    class KeyInput 
    {
        public string actionName = "New Action";
        [Space]
        public KeyState keyState;
        [Space]
        public KeyCode[] keys;
        [Space]
        public UnityEvent functionsToCall;

        public void CheckForInput()
        {
            foreach (var key in keys)
            {
                switch (keyState)
                {
                    case KeyState.KeyDown:
                        if (Input.GetKeyDown(key))
                        {
                            functionsToCall.Invoke();
                            return;
                        }
                        break;
                    case KeyState.Key:
                        if (Input.GetKey(key))
                        {
                            functionsToCall.Invoke();
                            return;
                        }
                        break;
                    case KeyState.KeyUp:
                        if (Input.GetKeyUp(key))
                        {
                            functionsToCall.Invoke();
                            return;
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }

    [System.Serializable]
    class AxisInput
    {
        [System.Serializable]
        public class AxisEvent : UnityEvent<float>
        {

        }


        public string actionName = "New Action";
        [Space]
        public string[] axisNames;
        [Space]
        public AxisEvent functionsToCall;

        public void CheckForInput()
        {
            foreach (var axisName in axisNames)
            {
                functionsToCall.Invoke(Input.GetAxis(axisName));
            }
        }
    }

    [SerializeField] private KeyInput[] keyInputs;
    [Space]
    [SerializeField] private AxisInput[] axisInputs;

    void Update()
    {
        foreach (var keyInput in keyInputs)
        {
            keyInput.CheckForInput();
        }

        foreach (var axisInput in axisInputs)
        {
            axisInput.CheckForInput();
        }
    }
}
