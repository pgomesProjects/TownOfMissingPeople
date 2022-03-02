using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    [SerializeField] private GameObject pauseUI;
    private PlayerControls playerControls;

    void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.Player.Pause.performed += _ => TogglePause();
    }

    // Start is called before the first frame update
    void Start()
    {
        InGameManager.instance.Paused = false;
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
        if (!InGameManager.instance.Paused)
            PauseGame();
        else
            ResumeGame();
    }

    public void PauseGame()
    {
        InGameManager.instance.Paused = true;
        pauseUI.SetActive(InGameManager.instance.Paused);
        Time.timeScale = 0.0f;
    }

    public void ResumeGame()
    {
        InGameManager.instance.Paused = false;
        pauseUI.SetActive(InGameManager.instance.Paused);
        Time.timeScale = 1.0f;
    }

    public void SettingsMenu()
    {

    }

    public void ReturnToMain()
    {
        Time.timeScale = 1.0f;
        LevelManager.instance.LoadScene("TitleScreen");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
