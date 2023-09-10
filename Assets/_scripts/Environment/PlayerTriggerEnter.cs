using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggerEnter : MonoBehaviour
{
    public delegate void AreaDamagePlayer(int damageAmount);
    public static event AreaDamagePlayer OnAreaDamagePlayer;

    [SerializeField] private int _damageAmount = 5;
    [SerializeField] private float _damageInterval = 1f;

    private bool playerInsideTrigger = false;

    // Start is called before the first frame update
    void Start()
    {
        if (_damageInterval > 0)
        {
            InvokeRepeating(nameof(DealDamageToPlayer), 0f, _damageInterval);
        }
    }

    private void DealDamageToPlayer()
    {
        if (playerInsideTrigger)
        {
            OnAreaDamagePlayer?.Invoke(_damageAmount);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInsideTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInsideTrigger = false;
        }
    }
}
