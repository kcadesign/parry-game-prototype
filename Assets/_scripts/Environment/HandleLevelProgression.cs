using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleLevelProgression : MonoBehaviour
{
    public delegate void SendCurrentCheckpoint(Vector3 currentCheckpoint, GameObject checkpointActivator);
    public static event SendCurrentCheckpoint OnSendCurrentCheckpoint;

    private Transform currentCheckpoint;
    public Transform LevelStart;

    void Awake()
    {
        currentCheckpoint = LevelStart;
    }

    private void OnEnable()
    {
        RespawnTrigger.OnPlayerLedgeFall += RespawnTrigger_OnPlayerLedgeFall;
    }

    private void OnDisable()
    {
        RespawnTrigger.OnPlayerLedgeFall -= RespawnTrigger_OnPlayerLedgeFall;
    }

    private void RespawnTrigger_OnPlayerLedgeFall(GameObject player)
    {
        player.transform.position = currentCheckpoint.position;
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
