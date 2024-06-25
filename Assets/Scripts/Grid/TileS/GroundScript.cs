/**
 * Script of the Ground tile, upon which the player can walk and push the crate
 */

public class GroundScript : TileScript
{
    public override bool IsOccupied()
    {
        if (transform.childCount > 0)
        {
            return true;
        }
        return false;
    }
    
    public override TileOccupier GetOccupier()
    {
        return transform.GetComponentInChildren<TileOccupier>();
    }
}
