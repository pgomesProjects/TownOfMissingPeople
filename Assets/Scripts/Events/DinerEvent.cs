using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DinerEvent : NPCDialogEvent
{
    private void Awake()
    {
        messageText = transform.Find("DialogCanvas").Find("TextPanel").Find("Dialog").Find("DialogText").GetComponent<TextMeshProUGUI>();
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
        string message;

        //Show different dialog based on whether the player has talked to the NPC before
        if(hasVisited)
            message = hasVisitedLines[currentLine];
        else
            message = dialogLines[currentLine];
        currentLine++;
        StartTalkingSound();
        continueObject.SetActive(false);

        switch (currentLine)
        {
            default:
                textWriterObj = TextWriter.AddWriter_Static(null, messageText, message, .05f, true, true, StopTalkingSound);
                break;
        }
    }

    public override void OnEventComplete()
    {
        //If the player has not visited them before, set to true
        if (!hasVisited)
            hasVisited = true;

        GameManager.instance.talkedToNPCs[1] = true;

        //Hide the dialog box and continue object
        messageText.gameObject.SetActive(false);
        continueObject.SetActive(false);
    }
}
