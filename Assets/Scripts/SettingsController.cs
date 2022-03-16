using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsController : MonoBehaviour
{
    public Toggle fullScreenToggle;
    public Slider volumeSlider;
    public TMP_Dropdown resDropdown;
    public TMP_Dropdown qualityDropdown;
    [HideInInspector]
    public int[,] possibleResolutions = new int[,]{ {2560, 1440}, {1920, 1080}, {1280, 720} };

    public void FullScreenToggle(bool isFullScreen)
    {
        //Toggle fullscreen variable
        GameManager.instance.currentSettings.SetIsFullScreen(isFullScreen);

        if (GameManager.instance.currentSettings.GetIsFullScreen())
        {
            Debug.Log("Game Is Fullscreen");
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        }
        else
        {
            Debug.Log("Game Is Windowed");
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }
    }//end of FullScreenToggle

    public void ChangeMusicVolume(float value)
    {
        Debug.Log("Volume: " + value * 0.1f);
        PlayerPrefs.SetFloat("MasterVolume", value * 0.1f);
        FindObjectOfType<AudioManager>().ChangeVolume(GameManager.instance.playingSongName, PlayerPrefs.GetFloat("MasterVolume"));
    }

    public void ResolutionDropdownChange(int resIndex)
    {
        GameManager.instance.currentSettings.SetCurrentResolution((GameSettings.Resolution)resIndex);
        Screen.SetResolution(possibleResolutions[(int)GameManager.instance.currentSettings.GetCurrentResolution(), 0], 
            possibleResolutions[(int)GameManager.instance.currentSettings.GetCurrentResolution(), 1], Screen.fullScreenMode);

        Debug.Log(GameManager.instance.currentSettings.GetCurrentResolution());
    }//end of ResolutionDropdownChange

    public void QualityDropdownChange(int qualityIndex)
    {
        GameManager.instance.currentSettings.SetGraphicsQuality((GameSettings.GraphicsQuality)qualityIndex);
        QualitySettings.SetQualityLevel((int)GameManager.instance.currentSettings.GetGraphicsQuality());
        Debug.Log(GameManager.instance.currentSettings.GetGraphicsQuality());
    }//end of QualityDropdownChange
}
