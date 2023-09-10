using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleCheckpoint : MonoBehaviour
{
    private HandleLevelProgression levelProgression;

    private void Start()
    {
        levelProgression = transform.parent.GetComponent<HandleLevelProgression>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log($"Player passed through checkpoint: {gameObject.name}");

            // Set this checkpoint as the most recent one in the parent script
            levelProgression.SetCurrentCheckpoint(transform, collision.gameObject);
        }
    }
}
