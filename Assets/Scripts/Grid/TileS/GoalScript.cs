using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : TileScript
{
    private bool _isOccupied;
    private bool _isFilled;

    public bool IsOccupied()
    {
        if (transform.childCount > 0)
        {
            return true;
        }
        return false;
    }

    public bool IsFilled()
    {
        if (!IsOccupied())
        {
            return false;
        }
        if (transform.GetComponentInChildren<TileOccupier>().IsPlayer)
        {
            return false;
        }

        return true;
    }

    public TileOccupier GetOccupier()
    {
        return transform.GetComponentInChildren<TileOccupier>();
    }
}
