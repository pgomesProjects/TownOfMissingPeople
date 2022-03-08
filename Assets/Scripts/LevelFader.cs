using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFader : MonoBehaviour
{

    public static LevelFader instance;
    public Animator animator;
    public float fadeInSeconds = 1.0f;
    public float fadeOutSeconds = 1.0f;
    private string levelToLoad;

    void Awake()
    {
        instance = this;
        if(fadeInSeconds == 0)
        {
            animator.CrossFade("fade_in", 0f, 0, 1f);
        }
        else
            animator.speed = 1 / fadeInSeconds;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeToLevel(string levelName)
    {
        levelToLoad = levelName;

        if (fadeOutSeconds == 0)
        {
            animator.CrossFade("fade_out", 0f, 0, 1f);
        }
        else
            animator.speed = 1 / fadeOutSeconds;

        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        GameManager.instance.interactionActive = false;
        LevelManager.instance.LoadScene(levelToLoad);
    }
}
