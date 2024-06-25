/**
 * Script of the Wall tile, which blocks the movement of crates and the player
 */
public class WallScript : TileScript
{
    public override bool IsOccupied()
    {
        return true;
    }
}
