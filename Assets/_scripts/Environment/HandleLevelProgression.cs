using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleLevelProgression : MonoBehaviour
{
    public delegate void SendCurrentCheckpoint(Vector3 currentCheckpoint, GameObject checkpointActivator);
    public static event SendCurrentCheckpoint OnSendCurrentCheckpoint;

    private Transform currentCheckpoint;
    public Transform LevelStart;

    private bool _playerDead = false;

    void Awake()
    {
        currentCheckpoint = LevelStart;
    }

    private void OnEnable()
    {
        RespawnTrigger.OnPlayerLedgeFall += RespawnTrigger_OnPlayerLedgeFall;
        HandlePlayerHealth.OnPlayerDead += HandlePlayerHealth_OnPlayerDead;
    }

    private void OnDisable()
    {
        RespawnTrigger.OnPlayerLedgeFall -= RespawnTrigger_OnPlayerLedgeFall;
        HandlePlayerHealth.OnPlayerDead -= HandlePlayerHealth_OnPlayerDead;
    }

    private void RespawnTrigger_OnPlayerLedgeFall(GameObject player)
    {
        if (!_playerDead)
        {
            player.transform.position = currentCheckpoint.position;
        }
    }

    private void HandlePlayerHealth_OnPlayerDead()
    {
        _playerDead = true;
    }

    public void SetCurrentCheckpoint(Transform checkpoint, GameObject checkpointActivator)
    {
        currentCheckpoint = checkpoint;
        OnSendCurrentCheckpoint?.Invoke(GetRespawnPoint(), checkpointActivator);
    }

    public Vector3 GetRespawnPoint()
    {
        return currentCheckpoint.position;
    }
}
