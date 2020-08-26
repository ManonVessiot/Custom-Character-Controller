using UnityEngine;

[System.Serializable]
public class L_UseButton
{
    public B_ControllerEvents.ActionButtonAlias _action;
    public KeyCode _key;

    public delegate void ButtonUsed();
    public ButtonUsed ButtonIsUsed;

    B_ControllerEvents.ActionButtonAlias _actionSubscribed = B_ControllerEvents.ActionButtonAlias.Undefined;

    public L_UseButton(B_ControllerEvents.ActionButtonAlias action, KeyCode key)
    {
        _action = action;
        _key = key;
    }

    ~L_UseButton()
    {

    }

    public void Subscribe(ButtonUsed function)
    {
        if (_action != B_ControllerEvents.ActionButtonAlias.Undefined)
        {
            if (_actionSubscribed != B_ControllerEvents.ActionButtonAlias.Undefined)
            {
                Unsubscribe();
            }
            
            _actionSubscribed = _action;

            

            switch (_action)
            {
                case B_ControllerEvents.ActionButtonAlias.Pressed:
                    B_ControllerEvents.keyWasPressed[B_ControllerEvents._values.IndexOf(_key)] += Use;
                    break;
                case B_ControllerEvents.ActionButtonAlias.Down:
                    B_ControllerEvents.keyIsDown[B_ControllerEvents._values.IndexOf(_key)] += Use;
                    break;
                case B_ControllerEvents.ActionButtonAlias.Released:
                    B_ControllerEvents.keyWasReleased[B_ControllerEvents._values.IndexOf(_key)] += Use;
                    break;
            }

            ButtonIsUsed += function;
        }        
    }

    public void Unsubscribe(ButtonUsed function = null)
    {
        if (function != null)
        {
            ButtonIsUsed -= function;
        }
        else
        {
            switch (_actionSubscribed)
            {
                case B_ControllerEvents.ActionButtonAlias.Pressed:
                    B_ControllerEvents.keyWasPressed[B_ControllerEvents._values.IndexOf(_key)] -= Use;
                    break;
                case B_ControllerEvents.ActionButtonAlias.Down:
                    B_ControllerEvents.keyIsDown[B_ControllerEvents._values.IndexOf(_key)] -= Use;
                    break;
                case B_ControllerEvents.ActionButtonAlias.Released:
                    B_ControllerEvents.keyWasReleased[B_ControllerEvents._values.IndexOf(_key)] -= Use;
                    break;
            }
            _actionSubscribed = B_ControllerEvents.ActionButtonAlias.Undefined;
            ButtonIsUsed = null;
        }
    }

    public void Use()
    {
        if (ButtonIsUsed != null)
        {
            ButtonIsUsed();
        }
    }
}
