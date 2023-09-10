using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    private bool _pauseTime = false;

    private void OnEnable()
    {
        HandlePlayerHealth.OnHealthChange += HandlePlayerHealth_OnHealthChange;
        HandleEnterFinish.OnLevelFinish += HandleEnterFinish_OnLevelFinish;
        HandleGameStateUI.OnGameRestart += HandleGameStateUI_OnGameRestart;
    }


    private void OnDisable()
    {
        HandlePlayerHealth.OnHealthChange -= HandlePlayerHealth_OnHealthChange;
        HandleEnterFinish.OnLevelFinish -= HandleEnterFinish_OnLevelFinish;
        HandleGameStateUI.OnGameRestart -= HandleGameStateUI_OnGameRestart;

    }

    private void HandlePlayerHealth_OnHealthChange(int currentHealth, bool playerAlive)
    {
        if (!playerAlive)
        {
            _pauseTime = true;
        }
        else
        {
            _pauseTime = false;
        }
        HandlePauseTime();
    }

    private void HandleEnterFinish_OnLevelFinish(bool levelFinished)
    {
        if (levelFinished)
        {
            _pauseTime = true;
        }
        else
        {
            _pauseTime = false;
        }
        HandlePauseTime();
    }

    private void HandleGameStateUI_OnGameRestart(Vector3 respawnPosition)
    {
        _pauseTime = false;
        HandlePauseTime();
    }

    private void HandlePauseTime()
    {
        if (_pauseTime)
        {
            Time.timeScale = 0f;
        }
        else if (!_pauseTime)
        {
            Time.timeScale = 1f;
        }
    }    


}