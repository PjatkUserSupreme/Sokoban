using UnityEngine;

namespace Level
{
    public class Level: MonoBehaviour
    {
        [SerializeField] private int levelID;
        
        public void OnClick()
        {
            ViewManager.GetInstance().DisplayGameUI();
            LevelLoader.GetInstance().StartLevel(levelID);
        }
    }
}