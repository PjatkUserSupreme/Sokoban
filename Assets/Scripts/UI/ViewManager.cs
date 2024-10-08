using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewManager : MonoBehaviour
{
    private static ViewManager _instance;

    public static ViewManager GetInstance()
    {
        return _instance;
    }
    
    [SerializeField] private GameObject mainMenuUI;
    [SerializeField] private GameObject levelChoiceUI;
    [SerializeField] private GameObject gameUI;
    [SerializeField] private GameObject howToPlayUI;
    [SerializeField] private GameObject winScreenUI;
    
    void Start()
    {
        _instance = this;
    }

    private void ClearAll()
    {
        mainMenuUI.SetActive(false);
        levelChoiceUI.SetActive(false);
        gameUI.SetActive(false);
        howToPlayUI.SetActive(false);
        winScreenUI.SetActive(false);
    }

    public void DisplayMainMenu()
    {
        ClearAll();
        mainMenuUI.SetActive(true);
    }

    public void DisplayLevelChoice()
    {
        ClearAll();
        levelChoiceUI.SetActive(true);
    }
    
    public void DisplayGameUI()
    {
        ClearAll();
        gameUI.SetActive(true);
    }
    public void DisplayHowToPlay()
    {
        ClearAll();
        howToPlayUI.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
