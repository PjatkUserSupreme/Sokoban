/**
 * Script of the Goal tile, into which the player is supposed to push the crate
 */
public class GoalScript : TileScript
{
    private bool _isOccupied;
    private bool _isFilled;

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
