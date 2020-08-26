using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class B_ControllerEvents : MonoBehaviour
{
    public enum ActionButtonAlias
    {
        /// <summary>
        /// No action specified.
        /// </summary>
        Undefined,
        /// <summary>
        /// Button has been pressed.
        /// </summary>
        Pressed,
        /// <summary>
        /// Button is down.
        /// </summary>
        Down,
        /// <summary>
        /// Button has been released.
        /// </summary>
        Released
    }

    public static List<KeyCode> _values;

    public delegate void KeyPressed();
    public static KeyPressed[] keyWasPressed;

    public delegate void KeyDown();
    public static KeyDown[] keyIsDown;

    public delegate void KeyReleased();
    public static KeyReleased[] keyWasReleased;


    private void Awake()
    {
        if (_values == null || _values.Count == 0)
        {
            Debug.Log("Awake");
            _values = ((KeyCode[])System.Enum.GetValues(typeof(KeyCode))).ToList<KeyCode>();
            keyWasPressed = new KeyPressed[_values.Count];
            keyIsDown = new KeyDown[_values.Count];
            keyWasReleased = new KeyReleased[_values.Count];
        }       
    }

    private void Update()
    {
        for (int index = 0; index < _values.Count; index++)
        {
            KeyCode key = _values[index];

            if (Input.GetKeyDown(key))
            {
                if (keyWasPressed != null && index < keyWasPressed.Length && keyWasPressed[index] != null)
                {
                    keyWasPressed[index]();
                }  
            }
            if (Input.GetKey(key))
            {
                if (keyIsDown != null && index < keyIsDown.Length && keyIsDown[index] != null)
                {
                    keyIsDown[index]();
                }
            }
            if (Input.GetKeyUp(key))
            {
                if (keyWasReleased != null && index < keyWasReleased.Length && keyWasReleased[index] != null)
                {
                    keyWasReleased[index]();
                }
            }
        }
    }
}
