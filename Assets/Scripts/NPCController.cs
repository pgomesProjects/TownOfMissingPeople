using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(TextWriter))]
public class NPCController : MonoBehaviour
{
    private TextWriter.TextWriterSingle textWriterSingle;
    private PlayerControls playerControls;
    private NPCDialogEvent dialogEvent;
    private bool isTalking = false;

    private void Awake()
    {
        dialogEvent = GetComponent<NPCDialogEvent>();
        playerControls = new PlayerControls();
        playerControls.UI.Click.performed += _ =>
        {
            if (isTalking)
            {
                //If there is text being written already, write everything
                if (textWriterSingle != null && textWriterSingle.IsActive())
                    textWriterSingle.WriteAllAndDestroy();

                //If there is no text and there are still visited lines left, check for events needed to display the text
                else if (dialogEvent.GetHasVisited() && (dialogEvent.GetCurrentLine() < dialogEvent.GetVisitedLinesLength()))
                {
                    dialogEvent.CheckEvents(ref textWriterSingle);
                }

                //If there is no text and there are still new lines left, check for events needed to display the text
                else if (!dialogEvent.GetHasVisited() && (dialogEvent.GetCurrentLine() < dialogEvent.GetDialogLength()))
                {
                    dialogEvent.CheckEvents(ref textWriterSingle);
                }

                //If all of the text has been shown, call the event for when the text is complete
                else
                {
                    isTalking = false;
                    dialogEvent.OnEventComplete();
                    GameManager.instance.interactionActive = false;
                }
            }
        };
    }

    public void StartDialog()
    {
        isTalking = true;
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
