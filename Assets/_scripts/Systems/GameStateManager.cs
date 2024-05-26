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

    private bool _pauseTime = false;
    private bool _playerCanPause = true;

    private void Awake()
    {
        playerControls = new PlayerControls();
        HandlePauseTime(_pauseTime);
        //Debug.Log($"Player can pause: {_playerCanPause}");
    }

    private void OnEnable()
    {
        playerControls.Menus.Enable();

        playerControls.Menus.Pause.performed += Pause_performed;
        playerControls.Menus.Pause.canceled += Pause_canceled;

        HandlePlayerDeath.OnPlayerDeathAnimEnd += HandlePlayerDeath_OnPlayerDeathAnimEnd;
        HandleBossDeath.OnBossDeathAnimEnd += HandleBossDeath_OnBossDeathAnimEnd;
        HandleEnterFinish.OnPlayerParryFinish += HandleEnterFinish_OnLevelFinish;
        HandleGameStateUI.OnGameRestart += HandleGameStateUI_OnGameRestart;
        HandleGameStateUI.OnGoToMainMenu += HandleGameStateUI_OnGoToMainMenu;
    }

    private void OnDisable()
    {
        playerControls.Menus.Enable();

        playerControls.Menus.Pause.performed -= Pause_performed;
        playerControls.Menus.Pause.canceled -= Pause_canceled;

        HandlePlayerDeath.OnPlayerDeathAnimEnd -= HandlePlayerDeath_OnPlayerDeathAnimEnd;
        HandleBossDeath.OnBossDeathAnimEnd -= HandleBossDeath_OnBossDeathAnimEnd;
        HandleEnterFinish.OnPlayerParryFinish -= HandleEnterFinish_OnLevelFinish;
        HandleGameStateUI.OnGameRestart -= HandleGameStateUI_OnGameRestart;
        HandleGameStateUI.OnGoToMainMenu -= HandleGameStateUI_OnGoToMainMenu;
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

    private void HandleBossDeath_OnBossDeathAnimEnd()
    {
        // load current scene + 1
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene + 1);
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
        _playerCanPause = true;
        HandlePauseTime(_pauseTime);
    }

    private void HandleGameStateUI_OnGoToMainMenu()
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

    private void PauseTime()
    {
        Time.timeScale = 0f;
    }

    private void UnpauseTime()
    {
        Time.timeScale = 1f;
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}