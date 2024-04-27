using System.Collections.Generic;
using UnityEngine;

public class GridScript : MonoBehaviour
{
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
        _levelLoader = GetComponent<LevelLoader>();
        _crates = new List<TileOccupier>();
    }

    private void Start()
    {
        SetTileMap(_levelLoader.GetLevel());
    }
    
    private void SetTileMap(char[,] gridArray)
    {
        _offsetX = -(gridArray.GetLength(0) * tileSize)/2;
        _offsetY = (gridArray.GetLength(1) * tileSize)/2;
        
        _tiles = new TileScript[gridArray.GetLength(0), gridArray.GetLength(1)];
        for(int i = 0; i < gridArray.GetLength(1); i++)
        {
            for(int j = 0; j < gridArray.GetLength(0); j++)
            {
                _tiles[j, i] = DecodeTile(gridArray[j,i]);
                _tiles[j, i].Initialize(j, i, _offsetX + j * tileSize, _offsetY - i * tileSize);
            }
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
                crate.Initialize(groundScript);
                
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
                player.Initialize(groundScript);
                
                _player = player;
                return ground.GetComponent<GroundScript>();
            }
            default:
                return null;
        }
    }
}
