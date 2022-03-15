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
    public bool Paused;
    [HideInInspector]
    public bool userInputChanged = false;
    [HideInInspector]
    public bool interactionActive = false;
    [HideInInspector]
    public string playerSpawnName = "StartSpawn";

    [HideInInspector]
    public bool [] tutorialsViewed = { false };
    [HideInInspector]
    public enum Tutorial { WALKING };

    private PlayerSpawnPoint spawnManager;

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

}
