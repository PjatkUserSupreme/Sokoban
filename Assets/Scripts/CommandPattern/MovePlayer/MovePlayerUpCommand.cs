
namespace CommandPattern.MovePlayer
{
    public class MovePlayerUpCommand : MoveCommand
    {
        public override bool Execute()
        {
            return Grid.MovePlayer("UP");
        }

        public override bool Undo()
        {
            return Grid.MovePlayer("DOWN");
        }
    }
}
