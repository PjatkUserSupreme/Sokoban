using UnityEngine;
using UnityEngine.UI;

namespace Level
{
    public class Level: MonoBehaviour
    {
        [SerializeField] private int levelID;
        [SerializeField] private Button levelButton;
        
        public void OnClick()
        {
            ViewManager.GetInstance().DisplayGameUI();
            LevelLoader.GetInstance().StartLevel(levelID);
        }
    }
}