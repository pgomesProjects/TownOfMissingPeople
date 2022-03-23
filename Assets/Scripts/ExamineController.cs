using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TextWriter))]
public class ExamineController : MonoBehaviour
{
    public Canvas dialogBox;

    private TextWriter.TextWriterSingle textWriterSingle;
    private PlayerControls playerControls;
    private DialogEvent dialogEvent;
    private bool isTalking = false;

    private void Awake()
    {
        dialogEvent = GetComponent<DialogEvent>();
        playerControls = new PlayerControls();
        playerControls.UI.Click.performed += _ =>
        {
            if (isTalking)
            {
                //If there is text being written already, write everything
                if (textWriterSingle != null && textWriterSingle.IsActive())
                    textWriterSingle.WriteAllAndDestroy();

                //If there is no text and there are still lines left, check for events needed to display the text
                else if (dialogEvent.GetCurrentLine() < dialogEvent.GetDialogLength())
                    dialogEvent.CheckEvents(ref textWriterSingle);

                //If all of the text has been shown, call the event for when the text is complete
                else
                {
                    isTalking = false;
                    dialogBox.gameObject.SetActive(false);
                    dialogEvent.OnEventComplete();
                    GameManager.instance.interactionActive = false;
                }
            }
        };
    }

    public void StartDialog()
    {
        isTalking = true;
        dialogBox.gameObject.SetActive(true);
        dialogEvent.OnDialogStart(ref textWriterSingle);
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
