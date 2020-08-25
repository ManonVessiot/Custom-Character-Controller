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
                    B_ControllerEvents.keyWasPressed += Use;
                    break;
                case B_ControllerEvents.ActionButtonAlias.Down:
                    B_ControllerEvents.keyIsDown += Use;
                    break;
                case B_ControllerEvents.ActionButtonAlias.Released:
                    B_ControllerEvents.keyWasReleased += Use;
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
                    B_ControllerEvents.keyWasPressed -= Use;
                    break;
                case B_ControllerEvents.ActionButtonAlias.Down:
                    B_ControllerEvents.keyIsDown -= Use;
                    break;
                case B_ControllerEvents.ActionButtonAlias.Released:
                    B_ControllerEvents.keyWasReleased -= Use;
                    break;
            }
            _actionSubscribed = B_ControllerEvents.ActionButtonAlias.Undefined;
            ButtonIsUsed = null;
        }
    }

    void Use(KeyCode key)
    {
        if (key == _key && ButtonIsUsed != null)
        {
            ButtonIsUsed();
        }
    }
}
