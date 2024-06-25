using System;
using System.IO;
using UnityEngine;

namespace SaveSystem
{
    public class FileDataHandler
    {
        private string _dataPath = "";
        private string _dataFileName = "";

        public FileDataHandler(string dataPath, string dataFileName)
        {
            _dataPath = dataPath;
            _dataFileName = dataFileName;
        }

        public GameData LoadData()
        {
            var fullPath = Path.Combine(_dataPath, _dataFileName);
            GameData loadedData = null;
            if(File.Exists(fullPath))
            {
                try
                {
                    var dataToLoad = "";
                    using(var stream = new FileStream(fullPath, FileMode.Open))
                    {
                        using (var reader = new StreamReader(stream))
                        {
                            dataToLoad = reader.ReadToEnd();
                        }
                    }

                    loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
                }
                catch (Exception e)
                {
                    Debug.LogError($"Error occured when trying to load data from a file: {fullPath} \n {e}");
                }
            }

            return loadedData;
        }

        public void SaveData(GameData data)
        {
            var fullPath = Path.Combine(_dataPath, _dataFileName);
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
                var dataToStore = JsonUtility.ToJson(data, true);
                using(var stream = new FileStream(fullPath, FileMode.Create))
                {
                    using (var writer = new StreamWriter(stream))
                    {
                        writer.Write(dataToStore);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Error occured when trying to save data into a file: {fullPath} \n {e}");
            }
        }
    }
}