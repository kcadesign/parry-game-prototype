using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleDamageOut : MonoBehaviour
{
    public delegate void OutputDamage(GameObject damagerObject, GameObject objectToDamage, int damageAmount);
    public static event OutputDamage OnOutputDamage;

    public int _damageAmount = 5;
    
    private IParryable _damageDealer;

    private void Awake()
    {
        _damageDealer = GetComponent<IParryable>();
        //Debug.Log(_damageDealer);
        if (_damageDealer == null)
        {
            Debug.LogWarning("No collision handler component found on " + gameObject);
        }
    }
    
    private void OnEnable()
    {
        if (_damageDealer != null)
        {
            _damageDealer.OnDamageCollision += IDealDamage_OnDamage;
        }
    }

    private void OnDisable()
    {
        if (_damageDealer != null)
        {
            _damageDealer.OnDamageCollision -= IDealDamage_OnDamage;
        }
    }

    private void IDealDamage_OnDamage(GameObject targetObject)
    {
        SendDamage(gameObject, targetObject, _damageAmount);
        // if the game object has a parent, debug the parents name to console
        if (gameObject.transform.parent != null)
        {
            Debug.Log($"{_damageAmount} damage sent to {targetObject.tag} from {gameObject.transform.parent.name}");
        }
        else if (gameObject.transform.parent == null)
        {
            Debug.Log($"{_damageAmount} damage sent to {targetObject.tag} from {gameObject.name}");
        }
    }

    private void SendDamage(GameObject damagerObject, GameObject targetObject, int damageAmount)
    {
        //Debug.Log($"SendDamge function called");
        OnOutputDamage?.Invoke(gameObject, targetObject, damageAmount);
    }
}
