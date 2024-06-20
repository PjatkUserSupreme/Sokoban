using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChooser : MonoBehaviour
{
    [SerializeField] private GameObject content;
    [SerializeField] private GameObject levelContainerPrefab;

    public void OnEnable()
    {
        for (int i = content.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(content.transform.GetChild(i).gameObject);
        }

        var levels = LevelLoader.GetInstance().Levels;
        int highestCompleted = LevelLoader.GetInstance().HighestCompleted;

        for (int i = 0; i < levels.Count; i++)
        {
            GameObject container = Instantiate(levelContainerPrefab, content.transform);
            container.GetComponent<LevelContainer>().Setup(levels[i].Item1, 
                highestCompleted >= i, 
                highestCompleted >= i - 1, 
                i);
        }
    }
}
