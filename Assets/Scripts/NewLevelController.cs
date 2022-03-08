using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class NewLevelController : MonoBehaviour
{
    public bool isUnlocked;
    public string levelName;

    public void LoadToNextLevel()
    {
        if (isUnlocked)
        {
            LevelFader.instance.FadeToLevel(levelName);
        }
    }
}
