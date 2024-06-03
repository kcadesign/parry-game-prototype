using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public PlayerControls PlayerControls;

    public GameObject[] Buttons;

    private Vector2 _navigate;

    public GameObject LastSelectedButton;
    public int LastSelectedIndex;

    private void Awake()
    {
        PlayerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        PlayerControls.Menus.Enable();
        PlayerControls.Menus.Navigate.performed += Navigate_performed;

        GameStateManager.OnPauseButtonPressed += GameStateManager_OnPauseButtonPressed;

        StartCoroutine(SetSelectedAfterOneFrame());

        AnimateButtonIntro();
    }

    private void OnDisable()
    {
        PlayerControls.Menus.Disable();
        PlayerControls.Menus.Navigate.performed -= Navigate_performed;

        GameStateManager.OnPauseButtonPressed -= GameStateManager_OnPauseButtonPressed;

        Debug.Log("Button Manager disabled");
    }

    private void Update()
    {
        HandleActiveSelector();
    }

    private void GameStateManager_OnPauseButtonPressed(bool playerPaused)
    {
        Debug.Log("Game paused: " + playerPaused);
        if (!playerPaused)
        {
            ResetButtonPosition();
        }
    }

    private void HandleActiveSelector()
    {
        // set Button selector to active on the currently selected button
        if (EventSystem.current.currentSelectedGameObject != null)
        {
            for (int i = 0; i < Buttons.Length; i++)
            {
                if (Buttons[i] == EventSystem.current.currentSelectedGameObject)
                {
                    Buttons[i].GetComponent<ButtonSelectionHandler>().ButtonSelector.SetActive(true);
                }
                else
                {
                    Buttons[i].GetComponent<ButtonSelectionHandler>().ButtonSelector.SetActive(false);
                }
            }
        }
    }

    private void AnimateButtonIntro()
    {
        
        // move the buttons off screen
        for (int i = 0; i < Buttons.Length; i++)
        {
            Buttons[i].transform.localPosition = new Vector3(0, Buttons[i].transform.localPosition.y - 1000, 0);
        }

        // move the buttons from off screen to their starting position using lean tween
        for (int i = 0; i < Buttons.Length; i++)
        {
            LeanTween.moveLocalY(Buttons[i], Buttons[i].transform.localPosition.y + 1000, 1f).setEase(LeanTweenType.easeOutExpo).setDelay(i * 0.25f).setIgnoreTimeScale(true);
        }
    }

    private void ResetButtonPosition()
    {
        // move the buttons off screen
        for (int i = 0; i < Buttons.Length; i++)
        {
            Buttons[i].transform.localPosition = new Vector3(0, Buttons[i].transform.localPosition.y, 0);
            // debug the position of each button
        }
    }

    private void Navigate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _navigate = obj.ReadValue<Vector2>();
        if (_navigate.y > 0)
        {
            HandleNextSelectedButton(1);
        }
        else if (_navigate.y < 0)
        {
            HandleNextSelectedButton(-1);
        }
    }

    private IEnumerator SetSelectedAfterOneFrame()
    {
        yield return null;
        EventSystem.current.SetSelectedGameObject(Buttons[0]);
        Buttons[0].GetComponent<ButtonSelectionHandler>().ButtonSelector.SetActive(true);
    }

    private void HandleNextSelectedButton(int addition)
    {
        if (EventSystem.current.currentSelectedGameObject == null && LastSelectedButton != null)
        {
            int newIndex = LastSelectedIndex + addition;
            newIndex = Mathf.Clamp(newIndex, 0, Buttons.Length - 1);
            EventSystem.current.SetSelectedGameObject(Buttons[newIndex]);
        }
    }
}
