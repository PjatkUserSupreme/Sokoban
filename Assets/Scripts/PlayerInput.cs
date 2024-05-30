using CommandPattern;
using CommandPattern.MovePlayer;
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
            _commandInvoker.ExecuteCommand(new MovePlayerUpCommand());
        }
    }

    public void OnDown(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _commandInvoker.ExecuteCommand(new MovePlayerDownCommand());
        }
    }

    public void OnLeft(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _commandInvoker.ExecuteCommand(new MovePlayerLeftCommand());
        }
    }

    public void OnRight(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _commandInvoker.ExecuteCommand(new MovePlayerRightCommand());
        }
    }

    public void OnUndo(InputAction.CallbackContext context)
    {
        _commandInvoker.UndoCommand();
    }
}
