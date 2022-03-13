using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IntroEvents : DialogEvent
{
    [SerializeField] private Image missingPoster;
    private RectTransform messageTransform, continueTransform;

    private void Awake()
    {
        messageText = transform.Find("DialogPanel").Find("Dialog").Find("DialogText").GetComponent<TextMeshProUGUI>();
        messageTransform = transform.Find("DialogPanel").Find("Dialog").Find("DialogText").GetComponent<RectTransform>();
        continueTransform = transform.Find("DialogPanel").Find("Dialog").Find("ContinueText").GetComponent<RectTransform>();
    }

    public override void CheckEvents(ref TextWriter.TextWriterSingle textWriterObj)
    {
        string message = dialogLines[currentLine];
        currentLine++;
        StartTalkingSound();
        continueObject.SetActive(false);

        switch (currentLine)
        {
            case 1:
                textWriterObj = TextWriter.AddWriter_Static(ShowPoster, messageText, message, .05f, true, true, StopTalkingSound);
                break;
            case 10:
                textWriterObj = TextWriter.AddWriter_Static(HidePoster, messageText, message, .05f, true, true, StopTalkingSound);
                break;
            case 15:
                textWriterObj = TextWriter.AddWriter_Static(ChangeToRed, messageText, message, .05f, true, true, StopTalkingSound);
                break;
            case 16:
                textWriterObj = TextWriter.AddWriter_Static(ChangeToWhite, messageText, message, .05f, true, true, StopTalkingSound);
                break;
            case 19:
                textWriterObj = TextWriter.AddWriter_Static(ItalicizeText, messageText, message, .05f, true, true, StopTalkingSound);
                break;
            default:
                textWriterObj = TextWriter.AddWriter_Static(null, messageText, message, .05f, true, true, StopTalkingSound);
                break;
        }
    }

    private void StartTalkingSound()
    {
        FindObjectOfType<AudioManager>().Play("TalkingNoise", 0.4f);
    }

    private void StopTalkingSound()
    {
        FindObjectOfType<AudioManager>().Stop("TalkingNoise");
        continueObject.SetActive(true);
    }

    private void ShowPoster()
    {
        //Show poster
        missingPoster.gameObject.SetActive(true);
        //Move position of the message box and change its width
        messageTransform.anchoredPosition = new Vector3(400, messageTransform.anchoredPosition.y);
        messageTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 1765);

        //Move position of the continue object
        continueTransform.anchoredPosition = new Vector3(398, -398);
    }

    private void HidePoster()
    {
        //Hide poster
        missingPoster.gameObject.SetActive(false);
        //Move position of the message box and change its width
        messageTransform.anchoredPosition = new Vector3(0, messageTransform.anchoredPosition.y);
        messageTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 3000);

        //Move position of the continue object
        continueTransform.anchoredPosition = new Vector3(0, -356);
    }

    private void ChangeToRed()
    {
        messageText.color = new Color32(255, 0, 0, 255);
    }

    private void ChangeToWhite()
    {
        messageText.color = new Color32(255, 255, 255, 255);
    }

    private void ItalicizeText()
    {
        messageText.fontStyle = TMPro.FontStyles.Italic;
    }

    public override void OnEventComplete()
    {
        continueObject.SetActive(false);
        LevelFader.instance.FadeToLevel("02_Outside");
    }

}
