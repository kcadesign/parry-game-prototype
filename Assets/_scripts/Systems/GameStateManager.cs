using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameStateManager : MonoBehaviour
{
    protected PlayerControls playerControls;

    public delegate void PlayerPause(bool playerPaused);
    public static event PlayerPause OnPlayerPause;

    private bool _pauseTime = false;
    private bool _playerCanPause = true;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Menus.Enable();

        playerControls.Menus.Pause.performed += Pause_performed;
        playerControls.Menus.Pause.canceled += Pause_canceled;

        HandlePlayerHealth.OnHealthChange += HandlePlayerHealth_OnHealthChange;
        HandleEnterFinish.OnLevelFinish += HandleEnterFinish_OnLevelFinish;
        HandleGameStateUI.OnGameRestart += HandleGameStateUI_OnGameRestart;
    }

    private void OnDisable()
    {
        playerControls.Menus.Enable();

        playerControls.Menus.Pause.performed -= Pause_performed;
        playerControls.Menus.Pause.canceled -= Pause_canceled;

        HandlePlayerHealth.OnHealthChange -= HandlePlayerHealth_OnHealthChange;
        HandleEnterFinish.OnLevelFinish -= HandleEnterFinish_OnLevelFinish;
        HandleGameStateUI.OnGameRestart -= HandleGameStateUI_OnGameRestart;
    }

    private void Pause_performed(InputAction.CallbackContext value)
    {
        //Debug.Log($"Pause button pressed");
        //Debug.Log($"Player can pause: {_playerCanPause}");

        if (_playerCanPause)
        {
            _pauseTime = !_pauseTime;
            HandlePauseTime(_pauseTime);
            OnPlayerPause?.Invoke(_pauseTime);
        }
    }

    private void Pause_canceled(InputAction.CallbackContext value)
    {
        return;
    }

    private void HandlePlayerHealth_OnHealthChange(int currentHealth, bool playerAlive)
    {
        if (!playerAlive)
        {
            _pauseTime = true;
            _playerCanPause = false;
        }
        else
        {
            _pauseTime = false;
            _playerCanPause = true;

        }
        HandlePauseTime(_pauseTime);
    }

    private void HandleEnterFinish_OnLevelFinish(bool levelFinished)
    {
        if (levelFinished)
        {
            _pauseTime = true;
            _playerCanPause = false;
        }
        else
        {
            _pauseTime = false;
            _playerCanPause = true;
        }
        HandlePauseTime(_pauseTime);
    }

    private void HandleGameStateUI_OnGameRestart(Vector3 respawnPosition)
    {
        _pauseTime = false;
        HandlePauseTime(_pauseTime);
    }

    private void HandlePauseTime(bool pauseTime)
    {
        if (pauseTime)
        {
            Time.timeScale = 0f;
        }
        else if (!pauseTime)
        {
            Time.timeScale = 1f;
        }
    }    

}