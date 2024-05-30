using System.Collections.Generic;

namespace CommandPattern
{
    public class CommandInvoker
    {
        private Stack<ICommand> _undoStack;

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
                _undoStack.Pop().Undo();
            }
        }
    }
}
