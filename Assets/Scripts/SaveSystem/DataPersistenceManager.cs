using UnityEngine;
using UnityEngine.SceneManagement;

namespace SaveSystem
{
    public class DataPersistenceManager : MonoBehaviour
    {
        [Header("Data Storage Config")]
        [SerializeField] private string fileName;
        public static DataPersistenceManager Instance { get; private set; }
        private GameData _gameData;
        private FileDataHandler _dataHandler;

        private void Awake()
        {
            if (Instance is not null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
            _dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            LoadGame();
        }
        
        public void LoadGame()
        {
            _gameData = _dataHandler.LoadData();

            if (_gameData is not null)
            {
                LevelLoader.GetInstance().HighestCompleted = _gameData.highestLevel;
                LevelLoader.GetInstance().UnlockLevels();
            }
        }

        public void SaveGame()
        {
            if (_gameData is null)
            {
                return;
            }
            
            _gameData.highestLevel = LevelLoader.GetInstance().HighestCompleted;
            _dataHandler.SaveData(_gameData);
        }

        private void OnApplicationQuit()
        {
            SaveGame();
        }

        public bool HasGameData()
        {
            return _gameData is not null;
        }
        public GameData GameData
        {
            get => _gameData;
            set => _gameData = value;
        }
    }
}