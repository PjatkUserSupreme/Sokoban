using UnityEngine;

public abstract class MoveCommand : ICommand
{
    protected Grid Grid;

    public abstract bool Execute();

    public abstract bool Undo();
}
