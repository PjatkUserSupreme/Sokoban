using System.Collections.Generic;
using UnityEngine;

public class GridScript : MonoBehaviour
{
    private static GridScript instance;

    public static GridScript GetInstance()
    {
        return instance;
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

    private void Start()
    {
    }
    
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

    private void Init()
    {
        _player.Initialize();
        foreach (var crate in _crates)
        {
            crate.Initialize();
        }
    }

    public TileScript GetTile(int x, int y)
    {
        return _tiles[x, y];
    }

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
                //TODO skrzynki
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
                //TODO gracz
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

    public bool IsMoveLegal(string direction)
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
                if (_tiles[_player.CoordX - 1, _player.CoordX] is null)
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


    public void MovePlayer(string direction)
    {
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
    }
    
    public void MoveCrate(string direction)
    {
        switch (direction)
        {
            case "UP":
            {
                TileOccupier crate = _tiles[_player.CoordX, _player.CoordY - 1].GetOccupier();
                crate.MoveToTile(_tiles[_player.CoordX, _player.CoordY - 2]);
                break;
            }
            case "DOWN":
            {
                TileOccupier crate = _tiles[_player.CoordX, _player.CoordY + 1].GetOccupier();
                crate.MoveToTile(_tiles[_player.CoordX, _player.CoordY + 2]);
                break;
            }
            
            case "LEFT":
            {
                TileOccupier crate = _tiles[_player.CoordX - 1, _player.CoordY].GetOccupier();
                crate.MoveToTile(_tiles[_player.CoordX - 2, _player.CoordY]);
                break;
            }
            case "RIGHT":
            {
                TileOccupier crate = _tiles[_player.CoordX + 1, _player.CoordY].GetOccupier();
                crate.MoveToTile(_tiles[_player.CoordX + 2, _player.CoordY]);
                break;
            }
        }
    }

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
