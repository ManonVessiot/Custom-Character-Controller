using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_SuscribeToUseButton : MonoBehaviour
{
    public L_UseButton useButton;


    private void OnEnable()
    {
        useButton.Subscribe(DebugUseButton);
    }
    private void OnDisable()
    {
        useButton.Unsubscribe();
    }


    void DebugUseButton()
    {
        Debug.Log("T_SuscribeToUseButton : button " + useButton._key + " " + useButton._action);
    }
}
