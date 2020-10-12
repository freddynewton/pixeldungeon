﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerHelper : MonoBehaviour
{
    public static GameManagerHelper Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // Disables all Logs in Builds to save performance
#if UNITY_EDITOR
        Debug.unityLogger.logEnabled = true;
#else
        Debug.unityLogger.logEnabled = false; 
#endif
    }


}