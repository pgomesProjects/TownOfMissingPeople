using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ExamineEvent : DialogEvent
{

    private void Awake()
    {
        messageText = transform.Find("ExamineCanvas").Find("ExaminePanel").Find("DialogText").GetComponent<TextMeshProUGUI>();
    }

    public override void OnDialogStart(ref TextWriter.TextWriterSingle textWriterObj)
    {
        currentLine = 0;
        //Set the text to be seen
        messageText.gameObject.SetActive(true);
        CheckEvents(ref textWriterObj);
    }

    public override void CheckEvents(ref TextWriter.TextWriterSingle textWriterObj)
    {
        string message = dialogLines[currentLine];
        currentLine++;
        continueObject.SetActive(false);

        switch (currentLine)
        {
            default:
                textWriterObj = TextWriter.AddWriter_Static(null, messageText, message, .05f, true, true, OnCompleteDialog);
                break;
        }
    }

    private void OnCompleteDialog()
    {
        continueObject.SetActive(true);
    }

    public override void OnEventComplete()
    {
        //Hide the dialog box and continue object
        messageText.gameObject.SetActive(false);
        continueObject.SetActive(false);
    }
}
