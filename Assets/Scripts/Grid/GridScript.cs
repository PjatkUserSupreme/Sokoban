using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GridScript : MonoBehaviour
{
    // Start is called before the first frame update
    private TileScript[,] _tiles;
    private LevelLoader _levelLoader;

    [SerializeField] private GameObject WallPrefab;
    [SerializeField] private GameObject GroundPrefab;
    [SerializeField] private GameObject GoalPrefab;
    [SerializeField] private float TileSize;
    [SerializeField] private float OffsetX;
    [SerializeField] private float OffsetY;
    
    void OnEnable()
    {
        _levelLoader = GetComponent<LevelLoader>();
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
        _tiles = new TileScript[gridArray.Length, gridArray.Length];
        for(int i = 0; i < gridArray.Length; i++)
        {
            for(int j = 0; j < gridArray.Length; j++)
            {
                _tiles[j, i] = DecodeTile(gridArray[j,i]);
                _tiles[j, i].Initialize(j, i, OffsetX + j * TileSize, OffsetY + i * TileSize);
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
                GameObject wall = Instantiate(WallPrefab);
                return wall.GetComponent<WallScript>();
            }
            case '.':
            {
                GameObject ground = Instantiate(GroundPrefab);
                return ground.GetComponent<GroundScript>();
            }
            case '*':
            {
                //TODO skrzynki
                GameObject ground = Instantiate(GroundPrefab);
                return ground.GetComponent<GroundScript>();
            }
            case 'o':
            {
                GameObject goal = Instantiate(GoalPrefab);
                return goal.GetComponent<GoalScript>();
            }
            case 'x':
            {
                //TODO gracz
                GameObject ground = Instantiate(GroundPrefab);
                return ground.GetComponent<GroundScript>();
            }
        }

        return null;
    }
    
}
