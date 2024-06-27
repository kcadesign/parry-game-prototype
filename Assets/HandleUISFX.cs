using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleUISFX : MonoBehaviour
{
    public SoundCollection UISounds;

    HandleUISFX Instance { get; set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        ButtonSelectionHandler.OnButtonSelected += ButtonSelectionHandler_OnButtonSelected;
        ButtonSelectionHandler.OnButtonpressed += ButtonSelectionHandler_OnButtonpressed;
    }

    private void OnDisable()
    {
        ButtonSelectionHandler.OnButtonSelected -= ButtonSelectionHandler_OnButtonSelected;
        ButtonSelectionHandler.OnButtonpressed -= ButtonSelectionHandler_OnButtonpressed;
    }

    private void ButtonSelectionHandler_OnButtonSelected()
    {
        //Debug.Log("Playing button selected SFX");
        UISounds.PlaySound("ButtonSelect", transform);
    }

    private void ButtonSelectionHandler_OnButtonpressed()
    {
        //Debug.Log("Playing button pressed SFX");
        UISounds.PlaySound("ButtonPress", transform);
    }
}
