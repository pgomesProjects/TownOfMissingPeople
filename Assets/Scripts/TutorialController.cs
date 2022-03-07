using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class TutorialController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textObject;
    [SerializeField] private string keyboardText;
    [SerializeField] private string controllerText;
    private PlayerControls playerControls;

    private enum CurrentController { KEYBOARD, CONTROLLER };
    private CurrentController currentControlScheme = CurrentController.KEYBOARD;

    public float disappearTime = 3f;

    private void Awake()
    {
        playerControls = new PlayerControls();
        InputUser.onChange += CheckController;
    }

    // Start is called before the first frame update
    void Start()
    {
        if(currentControlScheme == CurrentController.KEYBOARD)
            textObject.text = keyboardText;
        else if(currentControlScheme == CurrentController.CONTROLLER)
            textObject.text = controllerText;
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    public void CheckController(InputUser user, InputUserChange change, InputDevice device)
    {
        if (!GameManager.instance.Paused)
        {
            //Debug.Log(change);

            switch (change)
            {
                case InputUserChange.DevicePaired:

                    if (device != null)
                    {
                        if (device.description.manufacturer == "Sony Interactive Entertainment" && currentControlScheme != CurrentController.CONTROLLER)
                        {
                            currentControlScheme = CurrentController.CONTROLLER;
                            textObject.text = controllerText;
                        }

                        else if (device.displayName == "Keyboard" && currentControlScheme != CurrentController.KEYBOARD)
                        {
                            currentControlScheme = CurrentController.KEYBOARD;
                            textObject.text = keyboardText;
                        }
                    }

                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
            StartCoroutine(HideTutorial());

    }

    IEnumerator HideTutorial()
    {

        yield return new WaitForSeconds(disappearTime);

        Destroy(gameObject);
    }
}
