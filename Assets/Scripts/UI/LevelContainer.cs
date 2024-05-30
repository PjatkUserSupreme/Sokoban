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
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setup(string name, bool isCompleted, bool isAvailable, int levelId)
    {
        levelName.text = name;
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
