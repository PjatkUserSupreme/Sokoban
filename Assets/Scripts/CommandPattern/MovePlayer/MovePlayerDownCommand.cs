
namespace CommandPattern.MovePlayer
{
    public class MovePlayerDownCommand : MoveCommand
    {
        public override bool Execute()
        {
            return Grid.MovePlayer("DOWN");
        }

        public override bool Undo()
        {
            return Grid.MovePlayer("UP");
        }
    }
}