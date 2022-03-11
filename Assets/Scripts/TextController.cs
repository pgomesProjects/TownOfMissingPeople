using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(TextWriter))]
public class TextController : MonoBehaviour
{
    private TextWriter.TextWriterSingle textWriterSingle;
    private PlayerControls playerControls;
    private DialogEvent dialogEvent;
    private bool isDialogActive;

    public bool playOnStart;

    private void Awake()
    {
        isDialogActive = false;
        dialogEvent = GetComponent<DialogEvent>();
        playerControls = new PlayerControls();
        playerControls.UI.Click.performed += _ =>
        {
            //If the dialog is activated
            if (isDialogActive)
            {
                //If there is text being written already, write everything
                if (textWriterSingle != null && textWriterSingle.IsActive())
                    textWriterSingle.WriteAllAndDestroy();

                //If there is no text and there are still lines left, check for events needed to display the text
                else if (dialogEvent.GetCurrentLine() < dialogEvent.GetDialogLength())
                    dialogEvent.CheckEvents(ref textWriterSingle);

                //If all of the text has been shown, call the event for when the text is complete
                else
                    dialogEvent.OnEventComplete();
            }
        };
    }
    private void Start()
    {
        //If the dialog is meant to be played at the start, trigger it immediately
        if (playOnStart)
            TriggerDialogEvent();
    }

    public void TriggerDialogEvent()
    {
        //Start text event
        transform.Find("DialogPanel").gameObject.SetActive(true);
        isDialogActive = true;
        dialogEvent.CheckEvents(ref textWriterSingle);
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
