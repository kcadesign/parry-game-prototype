using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleCheckpoint : MonoBehaviour
{
    public static Action OnPassCheckpoint;
    public HandleLevelProgression levelProgression;

    private SpriteRenderer _spriteRenderer;
    public Color CheckpointSavedColor;

    private void Awake()
    {
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log($"Player passed through checkpoint: {gameObject.name}");

            // Set this checkpoint as the most recent one in the parent script
            levelProgression.SetCurrentCheckpoint(transform, collision.gameObject);

            _spriteRenderer.color = CheckpointSavedColor;
            OnPassCheckpoint?.Invoke();
        }
    }
}
