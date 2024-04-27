using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour, PlayerControl.IMovementActions
{
    private PlayerControl _control;
    private CommandInvoker _commandInvoker;
    
    private void Awake()
    {
        PrepareInputSystem();
    }
    private void PrepareInputSystem()
    {
        _commandInvoker = new CommandInvoker();
        _control = new PlayerControl();
            
        _control.Movement.SetCallbacks(this);
        _control.Movement.Enable();
    }
    
    
    public void OnUp(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _commandInvoker.ExecuteCommand(new MoveUpCommand());
        }
    }

    public void OnDown(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _commandInvoker.ExecuteCommand(new MoveDownCommand());
        }
    }

    public void OnLeft(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _commandInvoker.ExecuteCommand(new MoveLeftCommand());
        }
    }

    public void OnRight(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _commandInvoker.ExecuteCommand(new MoveRightCommand());
        }
    }

    public void OnUndo(InputAction.CallbackContext context)
    {
        _commandInvoker.UndoCommand();
    }
}
