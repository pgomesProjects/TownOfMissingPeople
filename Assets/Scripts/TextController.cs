using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class TextController : MonoBehaviour
{
    private TextMeshProUGUI messageText;
    private TextWriter.TextWriterSingle textWriterSingle;
    private AudioSource talkingAudioSource;
    private PlayerControls playerControls;

    private int currentDialogIndex = 0;
    public string[] dialogLines;
    public GameObject continueObject;

    private void Awake()
    {
        messageText = transform.Find("Dialog").Find("DialogText").GetComponent<TextMeshProUGUI>();
        talkingAudioSource = transform.Find("Dialog").GetComponent<AudioSource>();
        playerControls = new PlayerControls();
        playerControls.UI.Click.performed += _ =>
        {
            if (textWriterSingle != null && textWriterSingle.IsActive())
            {
                //Currently active TextWriter
                textWriterSingle.WriteAllAndDestroy();
            }
            else if (currentDialogIndex < dialogLines.Length)
            {
                string message = dialogLines[currentDialogIndex];
                currentDialogIndex++;
                StartTalkingSound();
                continueObject.SetActive(false);
                textWriterSingle = TextWriter.AddWriter_Static(messageText, message, .05f, true, true, StopTalkingSound);
            }
            else
            {
                continueObject.SetActive(false);
                LevelFader.instance.FadeToLevel("02_Outside");
            }
        };
    }

    private void Start()
    {
        //Message on start
        string message = dialogLines[currentDialogIndex];
        currentDialogIndex++;
        StartTalkingSound();
        continueObject.SetActive(false);
        textWriterSingle = TextWriter.AddWriter_Static(messageText, message, .05f, true, true, StopTalkingSound);
    }

    private void StartTalkingSound()
    {
        talkingAudioSource.Play();
    }

    private void StopTalkingSound()
    {
        talkingAudioSource.Stop();
        continueObject.SetActive(true);
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }
}
