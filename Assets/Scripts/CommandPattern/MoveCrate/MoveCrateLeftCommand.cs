
namespace CommandPattern.MoveCrate
{
    public class MoveCrateLeftCommand : MoveCommand
    {
        public override bool Execute()
        {
            return Grid.MoveCrate("LEFT");
        }

        public override bool Undo()
        {
            return Grid.MoveCrate("RIGHT");
        }
    }
}