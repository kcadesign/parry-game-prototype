using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleEnemyDamageOutput : MonoBehaviour
{
    public delegate void OutputDamage(int damageAmount);
    public static event OutputDamage OnOutputDamage;

    [SerializeField] private int _damageAmount = 5;

    private void OnEnable()
    {
        EnemyCollisionWithPlayer.OnDamagePlayer += EnemyCollisionWithPlayer_OnDamagePlayer;
    }

    private void OnDisable()
    {
        EnemyCollisionWithPlayer.OnDamagePlayer -= EnemyCollisionWithPlayer_OnDamagePlayer;
    }

    private void EnemyCollisionWithPlayer_OnDamagePlayer(bool damageConditionMet)
    {
        if (damageConditionMet)
        {
            SendDamage(_damageAmount);
            //Debug.Log($"Send {_damageAmount} damage");
        }
    }

    private void SendDamage(int damageAmount) => OnOutputDamage?.Invoke(damageAmount);
}