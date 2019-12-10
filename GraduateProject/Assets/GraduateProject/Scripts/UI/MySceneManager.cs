﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            goSampleScene();


    }

    static public void goEndScene()
    {
        SceneManager.LoadScene("EndScene");
    }
    static public void goSampleScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
