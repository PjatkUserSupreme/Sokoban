using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    private static LevelLoader instance;

    public static LevelLoader GetInstance()
    {
        return instance;
    }
    
    private int _currentLevel;
    private int _highestCompleted;
    private GridScript _gridScript;
    private List<Tuple<string, char[,]>> _levels;

    public void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void OnEnable()
    {
        LoadLevels();
        Debug.Log(_levels.Count);
        _gridScript = GetComponent<GridScript>();
        _currentLevel = 0;
        _highestCompleted = -1;
    }



    private void LoadLevels()
    {
        
        _levels = new List<Tuple<string, char[,]>>();
        for (int level = 1; level < 1000; level++)
        {
            TextAsset levelFile=(TextAsset)Resources.Load("level_" + level);
            if (levelFile is null)
            {
                break;
            }
            string levelString = levelFile.text;
            List<string> lines = new List<string>(levelFile.text.Split('\n'));
            string name = lines[0];
            char[,] map = new char[lines[^1].Length, lines.Count - 1];
            for(int i = 1; i < lines.Count; i++)
            {
                for(int j = 0; j < lines[^1].Length; j++)
                {
                    map[j, i - 1] = lines[i].ElementAt(j);
                }
            }
            _levels.Add(new Tuple<string, char[,]>(name, map));
        }
    }

    public List<Tuple<string, char[,]>> Levels => _levels;

    public void StartLevel(int i)
    {
        Debug.Log(_levels[i].Item1);
        _gridScript.SetTileMap(_levels[i].Item2);
    }

    public void StartCurrentLevel()
    {
        StartLevel(_currentLevel);
    }

    public void OnEndLevel()
    {
        _highestCompleted = Math.Max(_highestCompleted, _currentLevel);
        if (_currentLevel < _levels.Count - 1)
        {
            _currentLevel++;
            //TODO: WYSWIETL EKRAN SKONCZENIA POZIOMU
            StartCurrentLevel();    //TO PRZERZUCIC DO PRZYCISKU W TYM EKRANIE
        }
        else
        {
            Debug.Log("LAST LEVEL ENDED");
            //TODO: PO PRZEJSCIU WSZYSTKIEGO
        }
        
    }

    public int HighestCompleted => _highestCompleted;
}
