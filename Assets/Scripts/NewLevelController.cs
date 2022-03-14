using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class NewLevelController : MonoBehaviour
{
    public bool isUnlocked;
    public string levelName;
    public string playerSpawnName;
    public void LoadToNextLevel()
    {
        if (isUnlocked)
        {
            GameManager.instance.playerSpawnName = playerSpawnName;
            LevelFader.instance.FadeToLevel(levelName);
        }
    }
}
