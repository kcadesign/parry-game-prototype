using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlePlayerRespawn : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
/*    private void OnEnable()
    {
        HandleGameStateUI.OnRestartButtonPressed += HandleGameStateUI_OnGameRestart;
    }

    private void OnDisable()
    {
        HandleGameStateUI.OnRestartButtonPressed -= HandleGameStateUI_OnGameRestart;
    }

    private void HandleGameStateUI_OnGameRestart(Vector3 respawnPosition)
    {
        _rigidbody.velocity = Vector2.zero;

        gameObject.transform.position = respawnPosition;
    }
*/}
