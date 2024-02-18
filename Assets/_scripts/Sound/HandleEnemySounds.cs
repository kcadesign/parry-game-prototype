using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleEnemySounds : MonoBehaviour
{
    public SoundCollection _enemySounds;
    private AudioSource audioSource;

    private void Awake()
    {
        // Ensure there is an AudioSource component attached to this GameObject
        if (!TryGetComponent<AudioSource>(out audioSource))
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnEnable()
    {
        HandleEnemyHealth.OnEnemyDeath += HandleEnemyHealth_OnEnemyDeath;
    }

    private void OnDisable()
    {
        HandleEnemyHealth.OnEnemyDeath -= HandleEnemyHealth_OnEnemyDeath;
    }

    private void HandleEnemyHealth_OnEnemyDeath(GameObject deadEnemy)
    {
        if (deadEnemy == gameObject)
        {
            _enemySounds.PlaySound("Death", transform);

        }
    }

    

}
