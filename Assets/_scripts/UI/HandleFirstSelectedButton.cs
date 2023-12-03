using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HandleFirstSelectedButton : MonoBehaviour
{
    private EventSystem _eventSystem;

    private void Awake()
    {
        _eventSystem = GetComponent<EventSystem>();
    }

    private void OnEnable()
    {
        HandleGameStateUI.OnGameUIActivate += HandleGameStateUI_OnGameStateChange;
    }

    private void OnDisable()
    {
        HandleGameStateUI.OnGameUIActivate -= HandleGameStateUI_OnGameStateChange;
    }

    private void HandleGameStateUI_OnGameStateChange(GameObject firstSelectedButton)
    {
        _eventSystem.SetSelectedGameObject(firstSelectedButton);
    }
}
