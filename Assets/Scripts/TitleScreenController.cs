using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenController : MonoBehaviour
{
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
        SceneManager.LoadScene("01_Start");
    }//end of PlayGame

    public void SettingsMenu()
    {
        menuStates[(int)MenuState.SETTINGS] = !menuStates[(int)MenuState.SETTINGS];
        Debug.Log(menuStates[(int)MenuState.SETTINGS]);
    }//end of SettingsMenu

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }//end of QuitGame
}
