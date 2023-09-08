using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleGameStateUI : MonoBehaviour
{
    public GameObject GameOverUI;
    public GameObject LevelFinishUI;

    private void OnEnable()
    {
        HandlePlayerHealth.OnHealthChange += HandlePlayerHealth_OnHealthChange;
        HandleEnterFinish.OnLevelFinish += HandleEnterFinish_OnLevelFinish;
    }


    private void OnDisable()
    {
        HandlePlayerHealth.OnHealthChange -= HandlePlayerHealth_OnHealthChange;
        HandleEnterFinish.OnLevelFinish -= HandleEnterFinish_OnLevelFinish;

    }

    private void HandlePlayerHealth_OnHealthChange(int currentHealth)
    {
        if(currentHealth <= 0)
        {
            GameOverUI.SetActive(true);
        }
        else
        {
            GameOverUI.SetActive(false);
        }
    }    

    private void HandleEnterFinish_OnLevelFinish(bool levelFinished)
    {
        if (levelFinished)
        {
            LevelFinishUI.SetActive(true);
        }
        else
        {
            LevelFinishUI.SetActive(false);
        }
    }

}
