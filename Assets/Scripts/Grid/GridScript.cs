using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GridScript : MonoBehaviour
{
    // Start is called before the first frame update
    private List<string> _levelFiles;
    private LevelLoader _levelLoader;
    void OnEnable()
    {
        _levelLoader = GetComponent<LevelLoader>();
    }

    private void Start()
    {
        LoadLevel(_levelLoader.GetLevel());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void LoadLevel(string[,] gridArray)
    {
        
    }
    
    
    
}
