using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    [SerializeField] private GameObject pauseUI;
    private PlayerControls playerControls;
    private bool isPaused;

    void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.Player.Pause.performed += _ => TogglePause();
    }

    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
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
        if (!isPaused)
            PauseGame();
        else
            ResumeGame();
    }

    public void PauseGame()
    {
        isPaused = true;
        pauseUI.SetActive(isPaused);
        Time.timeScale = 0.0f;
    }

    public void ResumeGame()
    {
        isPaused = false;
        pauseUI.SetActive(isPaused);
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
