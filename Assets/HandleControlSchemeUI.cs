using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleControlSchemeUI : MonoBehaviour
{
    public GameObject KeyboardUI;
    public GameObject GamepadUI;

    private void OnEnable()
    {
        DetectInputDevice.OnControlSchemeChanged += DetectInputDevice_OnControlSchemeChanged;
    }

    private void OnDisable()
    {
        DetectInputDevice.OnControlSchemeChanged -= DetectInputDevice_OnControlSchemeChanged;
    }

    private void DetectInputDevice_OnControlSchemeChanged(string inputDevice)
    {
        if (inputDevice == "Keyboard")
        {
            KeyboardUI.SetActive(true);
            GamepadUI.SetActive(false);
        }
        else if (inputDevice == "Gamepad")
        {
            KeyboardUI.SetActive(false);
            GamepadUI.SetActive(true);
        }
    }
}
