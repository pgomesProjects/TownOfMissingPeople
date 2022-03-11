using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
abstract public class DialogEvent : MonoBehaviour
{
    protected TextMeshProUGUI messageText;
    protected int currentLine;
    [SerializeField] protected string[] dialogLines;
    [SerializeField] protected GameObject continueObject;

    public abstract void CheckEvents(ref TextWriter.TextWriterSingle textWriterObj);

    public int GetCurrentLine() { return this.currentLine; }
    public int GetDialogLength() { return this.dialogLines.Length; }

    public abstract void OnEventComplete();
}
