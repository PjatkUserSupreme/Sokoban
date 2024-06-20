using UnityEngine;

/**
 * Base script for all tiles placed in the level grid
 */
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

    /**
     * Setup the initial values of the tile
     *
     * <param name="coordX">Coordinate X in the level grid</param>
     * <param name="coordY">Coordinate Y in the level grid</param>
     * <param name="posX">World position X of the tile</param>
     * <param name="posY">World position Y of the tile</param>
     */
    public void Initialize(int coordX, int coordY, float posX, float posY)
    {
        gameObject.transform.position = new Vector3(posX, posY, 0);
        _coordX = coordX;
        _coordY = coordY;
    }

    /**
     * Checks the  occupation status of the tile
     *
     * <returns>Whether the tile is occupied</returns>
     */
    public virtual bool IsOccupied()
    {
        return true;
    }
    
    /**
     * <returns>The TileOccupier currently on the tile</returns>
     */
    public virtual TileOccupier GetOccupier()
    {
        return null;
    }

    public int CoordX => _coordX;

    public int CoordY => _coordY;
}