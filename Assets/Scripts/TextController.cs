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
        "This is the assistant speaking, hello and goodbye, see you next time!",
        "Hey there!",
        "This is a really cool and useful effect",
        "Let's learn some code and make awesome games!",
        "Goodbye for now!",
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
                SceneManager.LoadScene("02_Outside");
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
