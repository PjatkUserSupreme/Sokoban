using System.Collections.Generic;
using UnityEngine;

public class CommandInvoker : MonoBehaviour
{
    private Stack<ICommand> undoStack;

    private void ExecuteCommand(ICommand command)
    {
        command.Execute();
        undoStack.Push(command);
    }
    private void undoCommand()
    {
        undoStack.Pop().Undo();
    }
}
