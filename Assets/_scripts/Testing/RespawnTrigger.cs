using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnTrigger : MonoBehaviour
{
    public delegate void PlayerLedgeFall(GameObject checkpointActivator);
    public static event PlayerLedgeFall OnPlayerLedgeFall;

    private Transform _respawnPoint;

    /*
    private void OnEnable()
    {
        HandleLevelProgression.OnSendCurrentCheckpoint += HandleLevelProgression_OnSendCurrentCheckpoint;
    }

    private void OnDisable()
    {
        HandleLevelProgression.OnSendCurrentCheckpoint -= HandleLevelProgression_OnSendCurrentCheckpoint;
    }

    private void HandleLevelProgression_OnSendCurrentCheckpoint(Vector3 currentCheckpoint, GameObject checkpointActivator)
    {
        _respawnPoint.position = currentCheckpoint;
    }
    */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //collision.gameObject.transform.position = _respawnPoint.position;
            OnPlayerLedgeFall?.Invoke(collision.gameObject);
        }
    }
}
