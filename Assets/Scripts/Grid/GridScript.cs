using System.Collections.Generic;
using CommandPattern;
using UnityEngine;

/**
 * Script responsible for managing the state of the level grid
 */
public class GridScript : MonoBehaviour
{
    private static GridScript instance;
    private CommandInvoker _commandInvoker;

    public static GridScript GetInstance()
    {
        return instance;
    }

    public CommandInvoker CommandInvoker
    {
        set => _commandInvoker = value;
    }


    private TileScript[,] _tiles;
    private List<TileOccupier> _crates;
    private TileOccupier _player;
    private LevelLoader _levelLoader;

    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private GameObject groundPrefab;
    [SerializeField] private GameObject goalPrefab;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject cratePrefab;
    [SerializeField] private float tileSize;
    private float _offsetX;
    private float _offsetY;
    
    private void OnEnable()
    {
        instance = this;
        _levelLoader = GetComponent<LevelLoader>();
        _crates = new List<TileOccupier>();
    }
    
    /**
     * Sets the content of the tile map in accordance with the provided data
     *
     * <param name="gridArray">2D char array representing the level</param>
     */
    public void SetTileMap(char[,] gridArray)
    {
        _crates = new List<TileOccupier>();
        _tiles = new TileScript[gridArray.GetLength(0), gridArray.GetLength(1)];
        _player = null;
        for (int i = gameObject.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(gameObject.transform.GetChild(i).gameObject);
        }
        _offsetX = -(gridArray.GetLength(0) * tileSize)/2;
        _offsetY = (gridArray.GetLength(1) * tileSize)/2;
        
        
        for(int i = 0; i < gridArray.GetLength(1); i++)
        {
            for(int j = 0; j < gridArray.GetLength(0); j++)
            {
                _tiles[j, i] = DecodeTile(gridArray[j,i]);
                _tiles[j, i].Initialize(j, i, _offsetX + j * tileSize, _offsetY - i * tileSize);
            }
        }
        Init();
    }

    /**
     * Initialize the level by providing the loaded entities with appropriate data
     */
    private void Init()
    {
        _player.Initialize();
        foreach (var crate in _crates)
        {
            crate.Initialize();
        }
    }

    /**
     * Transforms the provided char into and appropriate tile type
     * #  - wall
     * . - ground
     * * - ground with crate
     * o - goal tile
     * x - ground with player
     * <param name="tileChar">Char representation of the file</param>
     */
    private TileScript DecodeTile(char tileChar)
    {
        switch (tileChar)
        {
            case '#':
            {
                GameObject wall = Instantiate(wallPrefab, transform);
                return wall.GetComponent<WallScript>();
            }
            case '.':
            {
                GameObject ground = Instantiate(groundPrefab, transform);
                return ground.GetComponent<GroundScript>();
            }
            case '*':
            {
                GameObject ground = Instantiate(groundPrefab, transform);
                GroundScript groundScript = ground.GetComponent<GroundScript>();
                
                TileOccupier crate = Instantiate(cratePrefab, ground.transform).GetComponent<TileOccupier>();
                
                _crates.Add(crate);
                return groundScript;
            }
            case 'o':
            {
                GameObject goal = Instantiate(goalPrefab, transform);
                return goal.GetComponent<GoalScript>();
            }
            case 'X':
            {
                GameObject ground = Instantiate(groundPrefab, transform);
                GroundScript groundScript = ground.GetComponent<GroundScript>();
                
                TileOccupier player = Instantiate(playerPrefab, ground.transform).GetComponent<TileOccupier>();
                
                _player = player;
                return ground.GetComponent<GroundScript>();
            }
            default:
                return null;
        }
    }

    /**
     * Checks whether the proposed player move is legal based on player position and surrounding tiles
     * <param name="direction">String specifying the direction of the move</param>
     * <returns>Is the move legal</returns>
     */
    private bool IsMoveLegal(string direction)
    {
        switch (direction)
        {
            case "UP":
            {
                if (_player.CoordY == 0)
                {
                    return false;
                }
                if (_tiles[_player.CoordX, _player.CoordY - 1] is null)
                {
                    return false;
                }

                if (_tiles[_player.CoordX, _player.CoordY - 1] is WallScript)
                {
                    return false;
                }

                if (_tiles[_player.CoordX, _player.CoordY - 1].IsOccupied())
                {
                    if (_player.CoordY == 1)
                    {
                        return false;
                    }
                    if (_tiles[_player.CoordX, _player.CoordY - 2] is null)
                    {
                        return false;
                    }
                    if (_tiles[_player.CoordX, _player.CoordY - 2].IsOccupied())
                    {
                        return false;
                    }
                    if (!_tiles[_player.CoordX, _player.CoordY - 2].IsOccupied())
                    {
                        return true;
                    }
                }

                return true;
            }
            case "DOWN":
            {
                if (_player.CoordY == _tiles.GetLength(1) - 1)
                {
                    return false;
                }
                if (_tiles[_player.CoordX, _player.CoordY + 1] is null)
                {
                    return false;
                }

                if (_tiles[_player.CoordX, _player.CoordY + 1] is WallScript)
                {
                    return false;
                }

                if (_tiles[_player.CoordX, _player.CoordY + 1].IsOccupied())
                {
                    if (_player.CoordY == _tiles.GetLength(1) - 2)
                    {
                        return false;
                    }
                    if (_tiles[_player.CoordX, _player.CoordY + 2] is null)
                    {
                        return false;
                    }
                    if (_tiles[_player.CoordX, _player.CoordY + 2].IsOccupied())
                    {
                        return false;
                    }
                    if (!_tiles[_player.CoordX, _player.CoordY + 2].IsOccupied())
                    {
                        return true;
                    }
                }
                return true;
            }
            
            case "LEFT":
            {
                
                if (_player.CoordX == 0)
                {
                    return false;
                }
                if (_tiles[_player.CoordX - 1, _player.CoordY] is null)
                {
                    return false;
                }

                if (_tiles[_player.CoordX - 1, _player.CoordY] is WallScript)
                {
                    return false;
                }

                if (_tiles[_player.CoordX - 1, _player.CoordY].IsOccupied())
                {
                    if (_player.CoordX == 1)
                    {
                        return false;
                    }
                    if (_tiles[_player.CoordX - 2, _player.CoordY] is null)
                    {
                        return false;
                    }
                    if (_tiles[_player.CoordX - 2, _player.CoordY].IsOccupied())
                    {
                        return false;
                    }
                    if (!_tiles[_player.CoordX - 2, _player.CoordY].IsOccupied())
                    {
                        return true;
                    }
                }
                return true;
            }
            case "RIGHT":
            {
                if (_player.CoordX == _tiles.GetLength(0) - 1)
                {
                    return false;
                }
                if (_tiles[_player.CoordX + 1, _player.CoordY] is null)
                {
                    return false;
                }

                if (_tiles[_player.CoordX + 1, _player.CoordY] is WallScript)
                {
                    return false;
                }

                if (_tiles[_player.CoordX + 1, _player.CoordY].IsOccupied())
                {
                    if (_player.CoordX == _tiles.GetLength(0) - 2)
                    {
                        return false;
                    }
                    if (_tiles[_player.CoordX + 2, _player.CoordY] is null)
                    {
                        return false;
                    }
                    if (_tiles[_player.CoordX + 2, _player.CoordY].IsOccupied())
                    {
                        return false;
                    }
                    if (!_tiles[_player.CoordX + 2, _player.CoordY].IsOccupied())
                    {
                        return true;
                    }
                }
                return true;
            }
        }
        
        return false;
    }
    
    /**
     * Moves the player in the specified direction if move is legal
     * <param name="direction">String specifying the direction of the move</param>
     * <returns>Whether the move has been performed</returns>
     */
    public bool MovePlayer(string direction)
    {
        var isMoveLegal = IsMoveLegal(direction);

        if (!isMoveLegal)
        {
            return false;
        }
        
        switch (direction)
        {
            case "UP":
            {
                _player.MoveToTile(_tiles[_player.CoordX, _player.CoordY - 1]);
                break;
            }
            case "DOWN":
            {
                _player.MoveToTile(_tiles[_player.CoordX, _player.CoordY + 1]);
                break;
            }
            
            case "LEFT":
            {
                _player.MoveToTile(_tiles[_player.CoordX - 1, _player.CoordY]);
                break;
            }
            case "RIGHT":
            {
                _player.MoveToTile(_tiles[_player.CoordX + 1, _player.CoordY]);
                break;
            }
        }
        Debug.Log("MOVE");
        return true;
    }
    
    /**
     * Checks whether a crate will be moved based on player position and surrounding tiles
     * <param name="direction">String specifying the direction of the move</param>
     * <returns>Will a crate move</returns>
     */
    public bool WillACrateMove(string direction)
    {
        switch (direction)
        {
            case "UP":
            {
                if (!(_tiles[_player.CoordX, _player.CoordY - 1] is WallScript))
                {
                    if (!_tiles[_player.CoordX, _player.CoordY - 1].IsOccupied())
                    {
                        return false;
                    }
                    if (!_tiles[_player.CoordX, _player.CoordY - 2].IsOccupied())
                    {
                        return true;
                    }
                }
                return false;
            }
            case "DOWN":
            {
                if (!(_tiles[_player.CoordX, _player.CoordY + 1] is WallScript))
                {
                    if (!_tiles[_player.CoordX, _player.CoordY + 1].IsOccupied())
                    {
                        return false;
                    }
                    if (!_tiles[_player.CoordX, _player.CoordY + 2].IsOccupied())
                    {
                        return true;
                    }
                }
                return false;
            }
            
            case "LEFT":
            {
                if (!(_tiles[_player.CoordX - 1, _player.CoordY] is WallScript))
                {
                    if (!_tiles[_player.CoordX - 1, _player.CoordY].IsOccupied())
                    {
                        return false;
                    }
                    if (!_tiles[_player.CoordX - 2, _player.CoordY].IsOccupied())
                    {
                        return true;
                    }
                }
                return false;
            }
            case "RIGHT":
            {
                if (!(_tiles[_player.CoordX + 1, _player.CoordY] is WallScript))
                {
                    if (!_tiles[_player.CoordX + 1, _player.CoordY].IsOccupied())
                    {
                        return false;
                    }
                    if (!_tiles[_player.CoordX + 2, _player.CoordY].IsOccupied())
                    {
                        return true;
                    }
                }
                return false;
            }
        }
        
        return false;
    }
    /**
     * Moves the crate next to the player in the specified direction if move is legal.
     * Also checks whether the victory conditions are fulfilled.
     * <param name="direction">String specifying the direction of the player move</param>
     * <returns>Whether the move has been performed</returns>
     */
    public bool MoveCrate(string direction)
    {
        var willACrateMove = WillACrateMove(direction);

        if (!willACrateMove)
        {
            return false;
        }
        
        switch (direction)
        {
            case "UP":
            {
                var crate = _tiles[_player.CoordX, _player.CoordY - 1].GetOccupier();
                crate.MoveToTile(_tiles[_player.CoordX, _player.CoordY - 2]);
                break;
            }
            case "DOWN":
            {
                var crate = _tiles[_player.CoordX, _player.CoordY + 1].GetOccupier();
                crate.MoveToTile(_tiles[_player.CoordX, _player.CoordY + 2]);
                break;
            }
            
            case "LEFT":
            {
                var crate = _tiles[_player.CoordX - 1, _player.CoordY].GetOccupier();
                crate.MoveToTile(_tiles[_player.CoordX - 2, _player.CoordY]);
                break;
            }
            case "RIGHT":
            {
                var crate = _tiles[_player.CoordX + 1, _player.CoordY].GetOccupier();
                crate.MoveToTile(_tiles[_player.CoordX + 2, _player.CoordY]);
                break;
            }
        }
        Debug.Log("MOVECRATE");
        if (IsLevelComplete())
        {
            Debug.Log("CLEAR");
            _commandInvoker.ClearStack();
            _levelLoader.OnEndLevel();
            return false;
        }
        return true;
    }
    /**
     * Reverses the crate movement based on the player position and performed move
     * <param name="movementToUndo">Movement performed by the crate to be undone</param>
     * <returns>Whether the undo has been succesful</returns>
     */
    public bool UndoMoveCrate(string movementToUndo)
    {
        
        switch (movementToUndo)
        {
            case "UP":
            {
                var crate = _tiles[_player.CoordX, _player.CoordY - 2].GetOccupier();
                crate.MoveToTile(_tiles[_player.CoordX, _player.CoordY - 1]);
                break;
            }
            case "DOWN":
            {
                var crate = _tiles[_player.CoordX, _player.CoordY + 2].GetOccupier();
                crate.MoveToTile(_tiles[_player.CoordX, _player.CoordY + 1]);
                break;
            }
            
            case "LEFT":
            {
                var crate = _tiles[_player.CoordX - 2, _player.CoordY].GetOccupier();
                crate.MoveToTile(_tiles[_player.CoordX - 1, _player.CoordY]);
                break;
            }
            case "RIGHT":
            {
                var crate = _tiles[_player.CoordX + 2, _player.CoordY].GetOccupier();
                crate.MoveToTile(_tiles[_player.CoordX + 1, _player.CoordY]);
                break;
            }
        }
        return true;
    }

    /**
     * Checks whether the victory conditions are fulfilled based on the state of goal tiles.
     * <returns>True if all goals are filled with crates</returns>
     */
    public bool IsLevelComplete()
    {
        foreach (var tileScript in _tiles)
        {
            if (tileScript is GoalScript)
            {
                if (tileScript.GetOccupier() is null)
                {
                    return false;
                }

                if (tileScript.GetOccupier().IsPlayer)
                {
                    return false;
                }
            }
        }
        
        return true;
    }
}
