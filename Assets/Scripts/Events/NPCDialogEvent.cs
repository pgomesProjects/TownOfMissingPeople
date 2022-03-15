using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPCDialogEvent : DialogEvent
{
    [SerializeField] protected string[] hasVisitedLines;
    protected bool hasVisited;
    [SerializeField] protected string talkingNoiseName;

    public int GetVisitedLinesLength() { return hasVisitedLines.Length; }
    public bool GetHasVisited() { return this.hasVisited; }

    public abstract void OnDialogStart(ref TextWriter.TextWriterSingle textWriterObj);

    protected void StartTalkingSound()
    {
        FindObjectOfType<AudioManager>().Play(talkingNoiseName, 0.4f);
    }

    protected void StopTalkingSound()
    {
        FindObjectOfType<AudioManager>().Stop(talkingNoiseName);
        continueObject.SetActive(true);
    }
}
