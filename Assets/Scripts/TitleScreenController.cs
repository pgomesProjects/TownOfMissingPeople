using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreenController : MonoBehaviour
{
    public GameObject[] gameMenus;
    private bool[] menuStates = {true, false};

    enum MenuState {MAIN, SETTINGS};

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void PlayGame()
    {
        LevelFader.instance.FadeToLevel("01_Start");
    }//end of PlayGame

    public void SettingsMenu()
    {
        //Toggle settings menu
        menuStates[(int)MenuState.SETTINGS] = !menuStates[(int)MenuState.SETTINGS];
        gameMenus[(int)MenuState.SETTINGS].SetActive(menuStates[(int)MenuState.SETTINGS]);

        //Toggle main menu
        menuStates[(int)MenuState.MAIN] = !menuStates[(int)MenuState.MAIN];
        gameMenus[(int)MenuState.MAIN].SetActive(menuStates[(int)MenuState.MAIN]);
    }//end of SettingsMenu

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }//end of QuitGame
}
