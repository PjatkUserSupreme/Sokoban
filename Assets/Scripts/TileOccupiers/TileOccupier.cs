using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileOccupier : MonoBehaviour
{
    private int _coordX, _coordY;
    
    
    public void Initialize(int coordX, int coordY, float posX, float posY)
    {
        gameObject.transform.position = new Vector3(posX, posY, 0);
        _coordX = coordX;
        _coordY = coordY;
    }

    public int CoordX => _coordX;

    public int CoordY => _coordY;

    public void MoveToTile(TileScript tileScript)
    {
        gameObject.transform.SetParent(tileScript.gameObject.transform);
        gameObject.transform.position = tileScript.gameObject.transform.position;
    }
}
