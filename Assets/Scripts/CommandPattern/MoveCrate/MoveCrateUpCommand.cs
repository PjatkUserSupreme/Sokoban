
namespace CommandPattern.MoveCrate
{
    public class MoveCrateUpCommand : MoveCommand
    {
        public override bool Execute()
        {
            return Grid.MoveCrate("UP");
        }

        public override bool Undo()
        {
            return Grid.UndoMoveCrate("UP");
        }
    }
}
