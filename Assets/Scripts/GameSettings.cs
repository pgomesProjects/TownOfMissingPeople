using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings
{
    private bool isFullScreen;
    private float masterVolume;
    private Resolution currentResolution;
    private GraphicsQuality graphicsQuality;

    [HideInInspector]
    public enum Resolution { RES1440P, RES1080P, RES720P };
    [HideInInspector]
    public enum GraphicsQuality { HIGH, MED, LOW };

    public GameSettings()
    {
        isFullScreen = false;
        masterVolume = 0.5f;
        currentResolution = Resolution.RES1080P;
        graphicsQuality = GraphicsQuality.HIGH;
    }//end of GameSettings

    public bool GetIsFullScreen() { return isFullScreen; }
    public void SetIsFullScreen(bool fullScreen) { isFullScreen = fullScreen; }

    public float GetMasterVolume() { return masterVolume; }
    public void SetMasterVolume(float volume) { masterVolume = volume; }

    public Resolution GetCurrentResolution() { return currentResolution; }
    public void SetCurrentResolution(Resolution res) { currentResolution = res; }

    public GraphicsQuality GetGraphicsQuality() { return graphicsQuality; }
    public void SetGraphicsQuality(GraphicsQuality quality) { graphicsQuality = quality; }
}
