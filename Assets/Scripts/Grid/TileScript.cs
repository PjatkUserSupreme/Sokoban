using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    private int _coordX, _coordY;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize(int coordX, int coordY, float posX, float posY)
    {
        gameObject.transform.position = new Vector3(posX, posY, 0);
        _coordX = coordX;
        _coordY = coordY;
    }

    public int CoordX => _coordX;

    public int CoordY => _coordY;
}

public class GroundScript : TileScript
{
    private bool _isOccupied;

    public bool IsOccupied
    {
        get => _isOccupied;
        set => _isOccupied = value;
    }
}

public class WallScript : TileScript
{
    
}

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