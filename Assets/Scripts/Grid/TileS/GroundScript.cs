using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    
    public TileOccupier GetOccupier()
    {
        return transform.GetComponentInChildren<TileOccupier>();
    }
}
