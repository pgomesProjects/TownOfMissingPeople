using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextControlUpdater : MonoBehaviour
{
    private TextMeshProUGUI textObject;
    [SerializeField] private string keyboardText;
    [SerializeField] private string controllerText;

    // Start is called before the first frame update
    void Start()
    {
        textObject = GetComponent<TextMeshProUGUI>();
        ChangeInputText();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeInputText();
    }

    private void ChangeInputText()
    {
        if (GameManager.instance.currentControlScheme == GameManager.CurrentController.KEYBOARD)
        {
            textObject.text = keyboardText;
        }
        else if (GameManager.instance.currentControlScheme == GameManager.CurrentController.CONTROLLER)
        {
            textObject.text = controllerText;
        }
    }
}
