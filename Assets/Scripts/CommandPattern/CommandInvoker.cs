using System.Collections.Generic;
using CommandPattern.MoveCrate;

namespace CommandPattern
{
    public class CommandInvoker
    {
        private Stack<ICommand> _undoStack = new Stack<ICommand>();

        public void ExecuteCommand(ICommand command)
        {
            var canExecute = command.Execute();

            if (canExecute)
            {
                _undoStack.Push(command);
            }
            
        }
        public void UndoCommand()
        {
            if (_undoStack.Count > 0)
            {
                ICommand command = _undoStack.Pop();
                command.Undo();
                if (_undoStack.Count == 0)
                {
                    return;
                }
                if (_undoStack.Peek() is MoveCrateDownCommand 
                    || _undoStack.Peek() is MoveCrateLeftCommand 
                    || _undoStack.Peek() is MoveCrateRightCommand 
                    || _undoStack.Peek() is MoveCrateUpCommand)
                {
                    UndoCommand();
                }
            }
        }
    }
}
