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

    private bool _timePaused = false;
    private bool _playerCanPause = true;

    private void Awake()
    {
        playerControls = new PlayerControls();
        HandleGamePause(_timePaused);
        //_currentScene = SceneManager.GetActiveScene();
        //Debug.Log($"Player can pause: {_playerCanPause}");
    }

    private void OnEnable()
    {
        playerControls.Gameplay.Enable();

        SceneManager.sceneLoaded += SceneManager_sceneLoaded;

        playerControls.Gameplay.Pause.performed += Pause_performed;
        playerControls.Gameplay.Pause.canceled += Pause_canceled;

        HandlePlayerDeath.OnPlayerDeathAnimEnd += HandlePlayerDeath_OnPlayerDeathAnimEnd;
        HandleEnterFinish.OnPlayerParryFinish += HandleEnterFinish_OnLevelFinish;
    }


    private void OnDisable()
    {
        playerControls.Gameplay.Enable();

        SceneManager.sceneLoaded -= SceneManager_sceneLoaded;

        playerControls.Gameplay.Pause.performed -= Pause_performed;
        playerControls.Gameplay.Pause.canceled -= Pause_canceled;

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
            _timePaused = !_timePaused;
            if (_timePaused)
            {
                PauseTime();
                OnPauseButtonPressed?.Invoke(_timePaused);
            }
            else if (!_timePaused)
            {
                UnpauseTime();
                OnPauseButtonPressed?.Invoke(_timePaused);
            }
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
            PauseTime();
            _playerCanPause = false;
        }
        else
        {
            UnpauseTime();
            _playerCanPause = true;
        }
    }


    private void HandleGamePause(bool pauseTime)
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