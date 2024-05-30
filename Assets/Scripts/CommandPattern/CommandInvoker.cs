using System.Collections.Generic;
using UnityEngine;

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
            }
        }
    }
}
