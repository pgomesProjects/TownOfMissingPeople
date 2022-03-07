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
                switch (currentDialogIndex)
                {
                    case 9:
                        textWriterSingle = TextWriter.AddWriter_Static(ChangeToRed, messageText, message, .05f, true, true, StopTalkingSound);
                        break;
                    case 10:
                        textWriterSingle = TextWriter.AddWriter_Static(ChangeToWhite, messageText, message, .05f, true, true, StopTalkingSound);
                        break;
                    default:
                        textWriterSingle = TextWriter.AddWriter_Static(null, messageText, message, .05f, true, true, StopTalkingSound);
                        break;
                }
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
        textWriterSingle = TextWriter.AddWriter_Static(null, messageText, message, .05f, true, true, StopTalkingSound);
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

    private void ChangeToRed()
    {
        messageText.color = new Color32(255, 0, 0, 255);
    }

    private void ChangeToWhite()
    {
        messageText.color = new Color32(255, 255, 255, 255);
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
