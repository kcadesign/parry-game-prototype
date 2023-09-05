using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleGameStateUI : MonoBehaviour
{
    public GameObject GameOverUI;

    private void OnEnable()
    {
        HandlePlayerHealth.OnHealthChange += HandlePlayerHealth_OnHealthChange;
    }

    private void OnDisable()
    {
        HandlePlayerHealth.OnHealthChange -= HandlePlayerHealth_OnHealthChange;
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
}
