using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlePlayerRespawn : MonoBehaviour
{
    private void OnEnable()
    {
        HandleGameStateUI.OnGameRestart += HandleGameStateUI_OnGameRestart;
    }

    private void OnDisable()
    {
        HandleGameStateUI.OnGameRestart -= HandleGameStateUI_OnGameRestart;
    }

    private void HandleGameStateUI_OnGameRestart(Vector3 respawnPosition)
    {
        gameObject.transform.position = respawnPosition;
    }
}
