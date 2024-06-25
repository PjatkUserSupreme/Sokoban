using UnityEngine;

/**
 * Class representing an entity which can reside inside a tile object
 */
public class TileOccupier : MonoBehaviour
{
    private int _coordX, _coordY;
    [SerializeField] private bool isPlayer;
    
    /**
     * Setup the appropriate values based on TileScript script attached to its parent.
     */
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

    /**
     * Moves the object to another tile object and changes values accordingly
     *
     * <param name="tileScript">Script attached to intended new parent object</param>
     */
    public void MoveToTile(TileScript tileScript)
    {
        gameObject.transform.SetParent(tileScript.gameObject.transform);
        _coordX = tileScript.CoordX;
        _coordY = tileScript.CoordY;
        gameObject.transform.position = tileScript.gameObject.transform.position;
    }
}
