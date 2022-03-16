using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
    private static PauseController instance;
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject settingsUI;
    private PlayerControls playerControls;
    private bool[] menuStates = { false, false };
    [SerializeField] private SettingsController settingsController;
    public Button resumeButton, backButton;

    enum MenuState { PAUSE, SETTINGS };

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

        playerControls = new PlayerControls();
        playerControls.Player.Pause.performed += _ => TogglePause();
        playerControls.UI.Cancel.performed += _ => ExitSettings();
    }

    // Start is called before the first frame update
    void Start()
    {
        CallSettingsValues();
        GameManager.instance.Paused = false;
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

    private void TogglePause()
    {
        if (!GameManager.instance.Paused && !GameManager.instance.interactionActive)
            PauseGame();
        else if(menuStates[(int)MenuState.PAUSE])
            ResumeGame();
    }

    public void PauseGame()
    {
        GameManager.instance.Paused = true;
        menuStates[(int)MenuState.PAUSE] = true;
        pauseUI.SetActive(GameManager.instance.Paused);
        Time.timeScale = 0.0f;
    }

    public void ResumeGame()
    {
        GameManager.instance.Paused = false;
        menuStates[(int)MenuState.PAUSE] = false;
        pauseUI.SetActive(GameManager.instance.Paused);
        Time.timeScale = 1.0f;
    }

    public void SettingsMenu()
    {
        //Toggle settings menu
        menuStates[(int)MenuState.SETTINGS] = !menuStates[(int)MenuState.SETTINGS];
        settingsUI.SetActive(menuStates[(int)MenuState.SETTINGS]);

        //If the settings is active, select the back button for controller navigation
        if (menuStates[(int)MenuState.SETTINGS])
            backButton.Select();
        else
            resumeButton.Select();

        //Toggle main menu
        menuStates[(int)MenuState.PAUSE] = !menuStates[(int)MenuState.PAUSE];
        pauseUI.SetActive(menuStates[(int)MenuState.PAUSE]);
    }

    private void ExitSettings()
    {
        //If in the settings menu, exit the settings menu with the settings toggle
        if (menuStates[(int)MenuState.SETTINGS])
        {
            SettingsMenu();
        }
    }//end of ExitSettings

    private void CallSettingsValues()
    {
        settingsController.fullScreenToggle.isOn = GameManager.instance.currentSettings.GetIsFullScreen();
        settingsController.volumeSlider.value = PlayerPrefs.GetFloat("MasterVolume") * 10;
        settingsController.resDropdown.value = (int)GameManager.instance.currentSettings.GetCurrentResolution();
        settingsController.resDropdown.RefreshShownValue();
        settingsController.qualityDropdown.value = (int)GameManager.instance.currentSettings.GetGraphicsQuality();
        settingsController.qualityDropdown.RefreshShownValue();
    }//end of CallSettingsValues

    public void ReturnToMain()
    {
        Time.timeScale = 1.0f;
        LevelManager.instance.LoadScene("TitleScreen");
        Destroy(gameObject);
    }

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
