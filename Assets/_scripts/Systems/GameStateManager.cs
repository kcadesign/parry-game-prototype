using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    protected PlayerControls playerControls;

    public delegate void PlayerPause(bool playerPaused);
    public static event PlayerPause OnPauseButtonPressed;

    //private Scene _currentScene;

    private bool _pauseTime = false;
    private bool _playerCanPause = true;

    private void Awake()
    {
        playerControls = new PlayerControls();
        HandlePauseTime(_pauseTime);
        //_currentScene = SceneManager.GetActiveScene();
        //Debug.Log($"Player can pause: {_playerCanPause}");
    }

    private void OnEnable()
    {
        playerControls.Menus.Enable();

        SceneManager.sceneLoaded += SceneManager_sceneLoaded;

        playerControls.Menus.Pause.performed += Pause_performed;
        playerControls.Menus.Pause.canceled += Pause_canceled;

        HandlePlayerDeath.OnPlayerDeathAnimEnd += HandlePlayerDeath_OnPlayerDeathAnimEnd;
        HandleEnterFinish.OnPlayerParryFinish += HandleEnterFinish_OnLevelFinish;
    }


    private void OnDisable()
    {
        playerControls.Menus.Enable();

        SceneManager.sceneLoaded -= SceneManager_sceneLoaded;

        playerControls.Menus.Pause.performed -= Pause_performed;
        playerControls.Menus.Pause.canceled -= Pause_canceled;

        HandlePlayerDeath.OnPlayerDeathAnimEnd -= HandlePlayerDeath_OnPlayerDeathAnimEnd;
        HandleEnterFinish.OnPlayerParryFinish -= HandleEnterFinish_OnLevelFinish;
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        UnpauseTime();
    }

    private void Pause_performed(InputAction.CallbackContext value)
    {
        //Debug.Log($"Pause button pressed");
        //Debug.Log($"Player can pause: {_playerCanPause}");

        if (_playerCanPause)
        {
            _pauseTime = !_pauseTime;
            HandlePauseTime(_pauseTime);
            OnPauseButtonPressed?.Invoke(_pauseTime);
        }
    }

    private void Pause_canceled(InputAction.CallbackContext value)
    {
        return;
    }

    private void HandlePlayerDeath_OnPlayerDeathAnimEnd()
    {
        //_pauseTime = true;
        _playerCanPause = false;
        PauseTime();

        //HandlePauseTime(_pauseTime);
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

    private void PauseTime()
    {
        Time.timeScale = 0f;
    }

    private void UnpauseTime()
    {
        Time.timeScale = 1f;
    }
}