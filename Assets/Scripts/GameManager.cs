using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Users;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [HideInInspector]
    public GameSettings currentSettings = new GameSettings();
    [HideInInspector]
    public bool Paused;
    [HideInInspector]
    public bool userInputChanged = false;
    [HideInInspector]
    public bool interactionActive = false;
    [HideInInspector]
    public string playerSpawnName = "StartSpawn";
    [HideInInspector]
    public string playingSongName = "";
    [HideInInspector]
    public List<bool> tutorialsViewed = new List<bool>{ false, false, false };
    [HideInInspector]
    public List<bool> talkedToNPCs = new List<bool> { false, false };
    [HideInInspector]
    public enum Tutorial { WALKING, PAUSE, INTERACTION };

    public enum CurrentController { NONE, KEYBOARD, CONTROLLER };
    public CurrentController currentControlScheme = CurrentController.KEYBOARD;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
    private void OnApplicationQuit()
    {
        Debug.Log("Quitting Game...");
        PlayerPrefs.SetInt("OnApplicationOpen", 0);
    }
}
