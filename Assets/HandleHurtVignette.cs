using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleHurtVignette : MonoBehaviour
{
    public GameObject HurtVignette;

    private void OnEnable()
    {
        HandlePlayerHealth.OnPlayerHurtSmall += HandlePlayerHealth_OnPlayerHurtSmall;
    }

    private void OnDisable()
    {
        HandlePlayerHealth.OnPlayerHurtSmall -= HandlePlayerHealth_OnPlayerHurtSmall;
    }

    private void HandlePlayerHealth_OnPlayerHurtSmall(bool hurt)
    {
        Debug.Log("Player hurt: " + hurt);
        if (hurt)
        {
            HurtVignette.SetActive(true);
        }
        else if (!hurt)
        {
            HurtVignette.SetActive(false);
        }
    }
}
