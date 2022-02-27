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
    private string[] messageArray = new string[]
    {
        "Greetings.",
        "You're really that desperate to find your wife, huh?",
        "Well, I can give you a place to start.",
        "Past the mountains, there's a small town of people.",
        "It's an odd town, but I think they'll be able to give you the answers you're looking for.",
        "I will say this, though.",
        "If you decide to go there...",
        "...",
        "Your life will never be the same again.",
        "Good luck. - Signed, Anonymous",
    };

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
            else if (currentDialogIndex < messageArray.Length)
            {
                string message = messageArray[currentDialogIndex];
                currentDialogIndex++;
                StartTalkingSound();
                textWriterSingle = TextWriter.AddWriter_Static(messageText, message, .05f, true, true, StopTalkingSound);
            }
            else
            {
                LevelFader.instance.FadeToLevel("02_Outside");
            }
        };
    }

    private void Start()
    {
        //Message on start
        string message = messageArray[currentDialogIndex];
        currentDialogIndex++;
        StartTalkingSound();
        textWriterSingle = TextWriter.AddWriter_Static(messageText, message, .05f, true, true, StopTalkingSound);
    }

    private void StartTalkingSound()
    {
        talkingAudioSource.Play();
    }

    private void StopTalkingSound()
    {
        talkingAudioSource.Stop();
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
