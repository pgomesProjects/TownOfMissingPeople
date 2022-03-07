using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCController : MonoBehaviour
{
    private TextMeshProUGUI messageText;
    private TextWriter.TextWriterSingle textWriterSingle;
    private AudioSource talkingAudioSource;
    private PlayerControls playerControls;

    private bool hasVisited = false;
    private bool isTalking = false;

    private int currentDialogIndex;
    private string[] dialogLines;
    public string[] newDialogLines;
    public string[] visitedDialogLines;
    public GameObject continueObject;

    private void Awake()
    {
        messageText = transform.Find("Dialog").Find("DialogText").GetComponent<TextMeshProUGUI>();
        talkingAudioSource = transform.Find("Dialog").GetComponent<AudioSource>();
        playerControls = new PlayerControls();
        playerControls.UI.Click.performed += _ =>
        {
            if (isTalking)
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
                    textWriterSingle = TextWriter.AddWriter_Static(null, messageText, message, .05f, true, true, StopTalkingSound);
                }
                else
                {
                    messageText.gameObject.SetActive(false);
                    continueObject.SetActive(false);
                    isTalking = false;
                    hasVisited = true;
                    GameManager.instance.interactionActive = false;
                }
            }
        };
    }

    public void StartDialog()
    {
        isTalking = true;

        //Choose which set of lines to read from
        if (hasVisited)
            dialogLines = visitedDialogLines;
        else
            dialogLines = newDialogLines;

        currentDialogIndex = 0;
        string message = dialogLines[currentDialogIndex];
        currentDialogIndex++;
        StartTalkingSound();
        
        //Show message and hide continue object
        messageText.gameObject.SetActive(true);
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

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }
}
