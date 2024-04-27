using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScript : TileScript
{
    private bool _isOccupied;

    public bool IsOccupied
    {
        get => _isOccupied;
        set => _isOccupied = value;
    }
    
    public TileOccupier GetOccupier()
    {
        return transform.GetComponentInChildren<TileOccupier>();
    }
}
