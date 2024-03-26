using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckpointVisual : MonoBehaviour
{
    public Image CheckpointIconSprite;
    [SerializeField] private float _blinkDuration = 3;

    private void OnEnable()
    {
        HandleCheckpoint.OnPassCheckpoint += HandleCheckpoint_OnPassCheckpoint;
    }

    private void OnDisable()
    {
        HandleCheckpoint.OnPassCheckpoint -= HandleCheckpoint_OnPassCheckpoint;
    }

    private void HandleCheckpoint_OnPassCheckpoint()
    {
        //Debug.Log("Checkpoint visual: checkpoint passed");
        StartCoroutine(BlinkCheckpointIcon());
    }

    private IEnumerator BlinkCheckpointIcon()
    {
        // Blink the checkpoint icon for X seconds
        float timePassed = 0;
        while (timePassed < _blinkDuration)
        {
            CheckpointIconSprite.enabled = !CheckpointIconSprite.enabled;
            yield return new WaitForSeconds(0.5f);
            timePassed += 0.5f;
        }
    }
}
