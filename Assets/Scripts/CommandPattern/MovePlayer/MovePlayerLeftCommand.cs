
namespace CommandPattern.MovePlayer
{
    public class MovePlayerLeftCommand : MoveCommand
    {
        public override bool Execute()
        {
            return Grid.MovePlayer("LEFT");
        }

        public override bool Undo()
        {
            return Grid.MovePlayer("RIGHT");
        }
    }
}