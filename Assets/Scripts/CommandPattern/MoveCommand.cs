using UnityEngine;

namespace CommandPattern
{
    public abstract class MoveCommand : ICommand
    {
        protected GridScript Grid = GridScript.GetInstance();

        public abstract bool Execute();

        public abstract bool Undo();
    }
}
