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

    public enum CurrentController { NONE, KEYBOARD, CONTROLLER };
    public CurrentController currentControlScheme = CurrentController.NONE;

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
