using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    private int _nextLevel;

    public void OnEnable()
    {
        _nextLevel = 1;
    }

    public string[,] GetLevel()
    {
        TextAsset levelFile=(TextAsset)Resources.Load("level_" + _nextLevel);
        string levelString = levelFile.text;
        List<String> lines = new List<string>(levelFile.text.Split('\n'));
        string[,] gridArray = new string[lines[0].Length, lines.Count];
        
        _nextLevel++;
        return gridArray;
    }
}
