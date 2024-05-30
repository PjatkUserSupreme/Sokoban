using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileOccupier : MonoBehaviour
{
    private int _coordX, _coordY;
    [SerializeField] private bool isPlayer;
    
    
    public void Initialize()
    {
        TileScript tileScript = GetComponentInParent<TileScript>();
        gameObject.transform.position = tileScript.gameObject.transform.position;
        _coordX = tileScript.CoordX;
        _coordY = tileScript.CoordY;
    }

    public int CoordX => _coordX;

    public int CoordY => _coordY;

    public bool IsPlayer => isPlayer;

    public void MoveToTile(TileScript tileScript)
    {
        gameObject.transform.SetParent(tileScript.gameObject.transform);
        _coordX = tileScript.CoordX;
        _coordY = tileScript.CoordY;
        gameObject.transform.position = tileScript.gameObject.transform.position;
    }
}
