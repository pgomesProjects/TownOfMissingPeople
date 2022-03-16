using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreenController : MonoBehaviour
{
    public GameObject[] gameMenus;
    private bool[] menuStates = {true, false};
    public Button playButton, backButton;
    private PlayerControls playerControls;
    enum MenuState {MAIN, SETTINGS};

    private void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.UI.Cancel.performed += _ => ReturnToMain();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void PlayGame()
    {
        FindObjectOfType<AudioManager>().Stop(GameManager.instance.playingSongName);
        LevelFader.instance.FadeToLevel("01_Start");
    }//end of PlayGame

    public void SettingsMenu()
    {
        //Toggle settings menu
        menuStates[(int)MenuState.SETTINGS] = !menuStates[(int)MenuState.SETTINGS];
        gameMenus[(int)MenuState.SETTINGS].SetActive(menuStates[(int)MenuState.SETTINGS]);

        //If the settings is active, select the back button for controller navigation
        if (menuStates[(int)MenuState.SETTINGS])
            backButton.Select();
        else
            playButton.Select();

        //Toggle main menu
        menuStates[(int)MenuState.MAIN] = !menuStates[(int)MenuState.MAIN];
        gameMenus[(int)MenuState.MAIN].SetActive(menuStates[(int)MenuState.MAIN]);
    }//end of SettingsMenu

    public void ReturnToMain()
    {
        //Return to main menu via the settings function
        if (menuStates[(int)MenuState.SETTINGS])
            SettingsMenu();
    }//end of ReturnToMain

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }//end of QuitGame
}
