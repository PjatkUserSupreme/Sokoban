using System;
using System.Collections.Generic;
using System.Linq;
using Level;
using SaveSystem;
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
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject nextLevelButton;
    private static LevelLoader instance;
    

    public static LevelLoader GetInstance()
    {
        return instance;
    }
    
    private int _currentLevel;
    private int _highestCompleted;
    private GridScript _gridScript;
    private Camera _camera;
    
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
        _camera = Camera.main;
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
         * Unlocks levels based on highestCompleted value
    */
    public void UnlockLevels()
    {
        for (int i = 0; i <= _highestCompleted + 1; i++)
        {
            levelButtons[i].interactable = true;
        }
    }

    /**
     * Load the level data for selected level. Creates new GameData if it doesn't exist
     * <param name="i">Position of the level in load order - 1</param>
     */
    public void StartLevel(int i)
    {
        if (!DataPersistenceManager.Instance.HasGameData())
        {
            DataPersistenceManager.Instance.GameData = new GameData();
        }
        ViewManager.GetInstance().DisplayGameUI();
        _gridScript.SetTileMap(_levels[i].Item2);
        _currentLevel = i;
        
        AdjustCamera();
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
        _highestCompleted = Math.Max(_highestCompleted, _currentLevel);
        if (_currentLevel < _levels.Count - 1)
        {
            nextLevelButton.SetActive(true);
            _currentLevel++;
            levelButtons[_currentLevel].interactable = true;
        }
        else
        {
            Debug.Log("LAST LEVEL ENDED");
            nextLevelButton.SetActive(false);
        }
        Time.timeScale = 0;
        winScreen.SetActive(true);
    }

    private void AdjustCamera()
    {
        Vector4 corners = _gridScript.GetDesiredCameraCorners();
        float centerX = (corners.y + corners.w) / 2;
        float centerY = (corners.x + corners.z) / 2;
        
        _camera.gameObject.transform.position = new Vector3(centerX, centerY, -1);
        float orthoSize = (Math.Abs(corners.y - corners.w) * 1.1f) * Screen.height / Screen.width * 0.5f;
        orthoSize = Math.Max(orthoSize, 4);
        _camera.orthographicSize = orthoSize;
    }

    public List<Tuple<string, char[,]>> Levels => _levels;
    
    public int CurrentLevel
    {
        get => _currentLevel;
        set => _currentLevel = value;
    }
    
    public int HighestCompleted
    {
        get => _highestCompleted;
        set => _highestCompleted = value;
    }
}
