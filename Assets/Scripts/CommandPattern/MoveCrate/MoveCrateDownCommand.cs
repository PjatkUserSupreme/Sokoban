
namespace CommandPattern.MoveCrate
{
    public class MoveCrateDownCommand : MoveCommand
    {
        public override bool Execute()
        {
            return Grid.MoveCrate("DOWN");
        }

        public override bool Undo()
        {
            return Grid.UndoMoveCrate("DOWN");
        }
    }
}
