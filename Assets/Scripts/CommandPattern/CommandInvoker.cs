using System.Collections.Generic;

public class CommandInvoker
{
    private Stack<ICommand> _undoStack;

    public void ExecuteCommand(ICommand command)
    {
        command.Execute();
        _undoStack.Push(command);
    }
    public void UndoCommand()
    {
        _undoStack.Pop().Undo();
    }
}
