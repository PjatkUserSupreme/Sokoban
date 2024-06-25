
namespace CommandPattern.MovePlayer
{
    public class MovePlayerRightCommand : MoveCommand
    {
        public override bool Execute()
        {
            return Grid.MovePlayer("RIGHT");
        }

        public override bool Undo()
        {
            return Grid.MovePlayer("LEFT");
        }
    }
}