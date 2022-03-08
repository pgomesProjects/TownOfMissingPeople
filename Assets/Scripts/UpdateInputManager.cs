using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Users;

public class UpdateInputManager : MonoBehaviour
{
    public InputUser user;

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.instance.currentControlScheme == GameManager.CurrentController.NONE)
        {
            // Create a user and give it the keyboard and the mouse
            user = InputUser.PerformPairingWithDevice(Keyboard.current);
            InputUser.PerformPairingWithDevice(Mouse.current, user: user);
            GameManager.instance.currentControlScheme = GameManager.CurrentController.KEYBOARD;
        }

        if (GameManager.instance.currentControlScheme == GameManager.CurrentController.KEYBOARD)
        {
            // Create a user and give it the keyboard and the mouse
            user.UnpairDevices();
            user = InputUser.PerformPairingWithDevice(Keyboard.current);
            InputUser.PerformPairingWithDevice(Mouse.current, user: user);
        }

        if (GameManager.instance.currentControlScheme == GameManager.CurrentController.CONTROLLER)
        {
            // Create a user and give it the gamepad and joystick
            user.UnpairDevices();
            user = InputUser.PerformPairingWithDevice(Gamepad.current);
            InputUser.PerformPairingWithDevice(Joystick.current, user: user);
        }

        // Listen for any use of a device that isn't paired to a user.
        ++InputUser.listenForUnpairedDeviceActivity;
        InputUser.onUnpairedDeviceUsed += (control, eventPtr) =>
        {
            var device = control.device;
            Debug.Log("New Device Detected: Is Gamepad? - " + (device is Gamepad));
            if (device is Gamepad || device is Joystick)
            {

                Debug.Log("Swapped To Gamepad and Joystick!");

                if (Keyboard.current != null)
                    user.UnpairDevice(Keyboard.current);
                if (Mouse.current != null)
                    user.UnpairDevice(Mouse.current);

                InputUser.PerformPairingWithDevice(device, user: user);
                GameManager.instance.currentControlScheme = GameManager.CurrentController.CONTROLLER;
            }

            if (device is Keyboard || device is Mouse)
            {

                Debug.Log("Swapped To Keyboard and Mouse!");

                if (Gamepad.current != null)
                    user.UnpairDevice(Gamepad.current);
                if (Joystick.current != null)
                    user.UnpairDevice(Joystick.current);

                InputUser.PerformPairingWithDevice(device, user: user);
                GameManager.instance.currentControlScheme = GameManager.CurrentController.KEYBOARD;
            }
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
