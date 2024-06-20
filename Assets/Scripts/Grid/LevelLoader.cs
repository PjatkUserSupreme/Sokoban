using System;
using System.Collections.Generic;
using System.Linq;
using Level;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

/**
 * Class responsible for keeping track of level data and which level has been completed as well as the level progression
 * logic.
 */
public class LevelLoader : MonoBehaviour
{
    [SerializeField] private List<Button> levelButtons;
    private static LevelLoader instance;

    public static LevelLoader GetInstance()
    {
        return instance;
    }
    
    private int _currentLevel;
    private int _highestCompleted;
    private GridScript _gridScript;
    /**
     * List of levels, which are stored as tuples containing level name and level map layout
     */
    private List<Tuple<string, char[,]>> _levels;

    public void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void OnEnable()
    {
        LoadLevels();
        _gridScript = GetComponent<GridScript>();
        _currentLevel = 0;
        _highestCompleted = -1;
    }

    /**
     * Loads all level files from Resources folder into the system.
     *
     * Level files must be named "level_X", where X is the level number in load order
     */
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

    
    

    /**
     * Load the level data for selected level
     * <param name="i">Position of the level in load order - 1</param>
     */
    public void StartLevel(int i)
    {
        ViewManager.GetInstance().DisplayGameUI();
        _gridScript.SetTileMap(_levels[i].Item2);
    }

    /**
     * Start level declared to be the current one now
     */
    public void StartCurrentLevel()
    {
        StartLevel(_currentLevel);
    }

    /**
     * Called after all victory conditions are fulfilled. Updates internal level count and enables appropriate UI
     */
    public void OnEndLevel()
    {
        LevelEvents.EndLevel(_currentLevel);
        _highestCompleted = Math.Max(_highestCompleted, _currentLevel);
        if (_currentLevel < _levels.Count - 1)
        {
            _currentLevel++;
            levelButtons[_currentLevel].interactable = true;
            //TODO: WYSWIETL EKRAN SKONCZENIA POZIOMU
            StartCurrentLevel();    //TO PRZERZUCIC DO PRZYCISKU W TYM EKRANIE
        }
        else
        {
            Debug.Log("LAST LEVEL ENDED");
            //TODO: PO PRZEJSCIU WSZYSTKIEGO
        }
        
    }

    public List<Tuple<string, char[,]>> Levels => _levels;
    public int HighestCompleted => _highestCompleted;
}
