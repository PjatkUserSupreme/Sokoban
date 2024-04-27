using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GridScript : MonoBehaviour
{
    // Start is called before the first frame update
    private TileScript[,] _tiles;
    private List<GameObject> _crates;
    private GameObject _player;
    private LevelLoader _levelLoader;

    [SerializeField] private GameObject WallPrefab;
    [SerializeField] private GameObject GroundPrefab;
    [SerializeField] private GameObject GoalPrefab;
    [SerializeField] private GameObject PlayerPrefab;
    [SerializeField] private GameObject CratePrefab;
    [SerializeField] private float TileSize;
    private float _offsetX;
    private float _offsetY;
    
    void OnEnable()
    {
        _levelLoader = GetComponent<LevelLoader>();
        _crates = new List<GameObject>();
    }

    private void Start()
    {
        SetTileMap(_levelLoader.GetLevel());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SetTileMap(char[,] gridArray)
    {
        _offsetX = -(gridArray.GetLength(0) * TileSize)/2;
        _offsetY = (gridArray.GetLength(1) * TileSize)/2;
        
        _tiles = new TileScript[gridArray.GetLength(0), gridArray.GetLength(1)];
        for(int i = 0; i < gridArray.GetLength(1); i++)
        {
            for(int j = 0; j < gridArray.GetLength(0); j++)
            {
                _tiles[j, i] = DecodeTile(gridArray[j,i]);
                _tiles[j, i].Initialize(j, i, _offsetX + j * TileSize, _offsetY - i * TileSize);
            }
        }
    }

    public TileScript GetTile(int x, int y)
    {
        return _tiles[x, y];
    }

    public TileScript DecodeTile(char tileChar)
    {
        switch (tileChar)
        {
            case '#':
            {
                GameObject wall = Instantiate(WallPrefab, transform);
                return wall.GetComponent<WallScript>();
            }
            case '.':
            {
                GameObject ground = Instantiate(GroundPrefab, transform);
                return ground.GetComponent<GroundScript>();
            }
            case '*':
            {
                //TODO skrzynki
                GameObject ground = Instantiate(GroundPrefab, transform);
                GameObject crate = Instantiate(CratePrefab, ground.transform);
                crate.transform.position = ground.transform.position;
                _crates.Add(crate);
                return ground.GetComponent<GroundScript>();
            }
            case 'o':
            {
                GameObject goal = Instantiate(GoalPrefab, transform);
                return goal.GetComponent<GoalScript>();
            }
            case 'X':
            {
                //TODO gracz
                GameObject ground = Instantiate(GroundPrefab, transform);
                GameObject player = Instantiate(PlayerPrefab, ground.transform);
                player.transform.position = ground.transform.position;
                _player = player;
                return ground.GetComponent<GroundScript>();
            }
        }

        return null;
    }
    
}
