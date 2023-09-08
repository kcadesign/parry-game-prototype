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
    }


    private void OnDisable()
    {
        HandlePlayerHealth.OnHealthChange -= HandlePlayerHealth_OnHealthChange;
        HandleEnterFinish.OnLevelFinish += HandleEnterFinish_OnLevelFinish;
    }
    private void HandlePlayerHealth_OnHealthChange(int currentHealth)
    {
        if (currentHealth <= 0)
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
