using System.Collections;
using System.Collections.Generic;
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

    KeyCode[] _values;

    public delegate void KeyPressed(KeyCode key);
    public static KeyPressed keyWasPressed;

    public delegate void KeyDown(KeyCode key);
    public static KeyDown keyIsDown;

    public delegate void KeyReleased(KeyCode key);
    public static KeyReleased keyWasReleased;


    private void Awake()
    {
        _values = (KeyCode[])System.Enum.GetValues(typeof(KeyCode));
    }

    private void Update()
    {
        foreach (KeyCode key in _values)
        {
            if (Input.GetKeyDown(key))
            {
                if (keyWasPressed != null)
                    keyWasPressed(key);
            }
            if (Input.GetKey(key))
            {
                if (keyIsDown != null)
                    keyIsDown(key);
            }
            if (Input.GetKeyUp(key))
            {
                if (keyWasReleased != null)
                    keyWasReleased(key);
            }
        }
    }
}
