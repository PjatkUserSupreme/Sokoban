using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    private int _nextLevel;

    public void OnEnable()
    {
        _nextLevel = 1;
    }

    public char[,] GetLevel()
    {
        TextAsset levelFile=(TextAsset)Resources.Load("level_" + _nextLevel);
        string levelString = levelFile.text;
        Debug.Log(levelString);
        List<string> lines = new List<string>(levelFile.text.Split('\n'));
        char[,] gridArray = new char[lines[lines.Count -1].Length, lines.Count];
        for(int i = 0; i < lines.Count; i++)
        {
            for(int j = 0; j < lines[lines.Count -1].Length; j++)
            {
                gridArray[j, i] = lines[i].ElementAt(j);
            }
        }
        
        _nextLevel++;
        return gridArray;
    }
}
