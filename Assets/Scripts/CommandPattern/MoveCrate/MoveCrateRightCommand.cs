
namespace CommandPattern.MoveCrate
{
    public class MoveCrateRightCommand : MoveCommand
    {
        public override bool Execute()
        {
            return Grid.MoveCrate("RIGHT");
        }

        public override bool Undo()
        {
            return Grid.UndoMoveCrate("RIGHT");
        }
    }
}