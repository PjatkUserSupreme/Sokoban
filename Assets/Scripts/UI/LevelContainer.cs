using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelContainer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelName;

    [SerializeField] private Image image;

    private Button _button;

    private int _levelID;

    public void Setup(string levelName, bool isCompleted, bool isAvailable, int levelId)
    {
        this.levelName.text = levelName;
        image.gameObject.SetActive(isCompleted);
        _button = GetComponent<Button>();
        _button.interactable = isAvailable;
        _levelID = levelId;
    }

    public void OnClick()
    {
        ViewManager.GetInstance().DisplayGameUI();
        LevelLoader.GetInstance().StartLevel(_levelID);
    }
}
