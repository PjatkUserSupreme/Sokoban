using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : TileScript
{
    private bool _isOccupied;
    private bool _isFilled;

    public bool IsOccupied
    {
        get => _isOccupied;
        set => _isOccupied = value;
    }

    public bool IsFilled
    {
        get => _isFilled;
        set => _isFilled = value;
    }
}
