using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SettingsOnStart : MonoBehaviour
{
    public SettingsController settingsController;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("OnApplicationOpen") == 0)
            BeginSetup();
        else
            CallSettingsValues();

        FindObjectOfType<AudioManager>().Play(GameManager.instance.playingSongName, PlayerPrefs.GetFloat("MasterVolume"));
    }

    private void BeginSetup()
    {
        if (PlayerPrefs.GetInt("FirstRun") == 0)
        {
            PlayerPrefs.SetFloat("MasterVolume", 0.5f);
            PlayerPrefs.SetInt("FirstRun", 1);
        }

        GameManager.instance.playingSongName = "TitlescreenMusic";

        SetUpFullscreen();
        SetUpVolume();
        SetUpResolution();
        SetUpQuality();

        PlayerPrefs.SetInt("OnApplicationOpen", 1);
    }//end of BeginSetup

    private void CallSettingsValues()
    {
        settingsController.fullScreenToggle.isOn = GameManager.instance.currentSettings.GetIsFullScreen();
        settingsController.volumeSlider.value = PlayerPrefs.GetFloat("MasterVolume") * 10;
        settingsController.resDropdown.value = (int)GameManager.instance.currentSettings.GetCurrentResolution();
        settingsController.resDropdown.RefreshShownValue();
        settingsController.qualityDropdown.value = (int)GameManager.instance.currentSettings.GetGraphicsQuality();
        settingsController.qualityDropdown.RefreshShownValue();
    }//end of CallSettingsValues

    private void SetUpFullscreen()
    {
        GameManager.instance.currentSettings.SetIsFullScreen(Screen.fullScreenMode == FullScreenMode.ExclusiveFullScreen);
        if (GameManager.instance.currentSettings.GetIsFullScreen())
            settingsController.fullScreenToggle.isOn = true;
        else
            settingsController.fullScreenToggle.isOn = false;
    }//end of SetUpFullscreen

    private void SetUpVolume()
    {
        settingsController.volumeSlider.value = PlayerPrefs.GetFloat("MasterVolume") * 10;
        FindObjectOfType<AudioManager>().ChangeVolume(GameManager.instance.playingSongName, PlayerPrefs.GetFloat("MusicVol"));
    }//end of SetUpVolume

    private void SetUpResolution()
    {
        int currentResolutionIndex = -1;

        if (GameManager.instance.currentSettings.GetIsFullScreen())
        {
            for (int i = 0; i < settingsController.possibleResolutions.GetLength(0); i++)
            {
                if (Screen.currentResolution.width == settingsController.possibleResolutions[i, 0]
                    && Screen.currentResolution.height == settingsController.possibleResolutions[i, 1])
                    currentResolutionIndex = i;
            }
        }
        else
        {
            for (int i = 0; i < settingsController.possibleResolutions.GetLength(0); i++)
            {
                if (Screen.width == settingsController.possibleResolutions[i, 0]
                    && Screen.height == settingsController.possibleResolutions[i, 1])
                    currentResolutionIndex = i;
            }
        }

        //Set to 1080p if none apply
        if (currentResolutionIndex == -1)
            currentResolutionIndex = 1;

        settingsController.resDropdown.value = currentResolutionIndex;
        GameManager.instance.currentSettings.SetCurrentResolution((GameSettings.Resolution)currentResolutionIndex);
        settingsController.resDropdown.RefreshShownValue();
    }//end of SetUpResolution

    private void SetUpQuality()
    {
        int currentQuality = QualitySettings.GetQualityLevel();
        settingsController.qualityDropdown.value = currentQuality;
        GameManager.instance.currentSettings.SetGraphicsQuality((GameSettings.GraphicsQuality)currentQuality);
        settingsController.qualityDropdown.RefreshShownValue();
    }//end of SetUpQuality

}
