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
        pauseUI.SetActive(isPaused);
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
        isPaused = !isPaused;

        if (isPaused)
            PauseGame();
        else
            ResumeGame();
    }

    public void PauseGame()
    {
        pauseUI.SetActive(isPaused);
        Time.timeScale = 0.0f;
    }

    public void ResumeGame()
    {
        pauseUI.SetActive(isPaused);
        Time.timeScale = 1.0f;
    }
}
