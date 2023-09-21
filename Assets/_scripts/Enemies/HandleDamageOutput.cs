using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleDamageOutput : MonoBehaviour
{
    public delegate void OutputDamage(GameObject collisionObject, int damageAmount);
    public static event OutputDamage OnOutputDamage;

    [SerializeField] private int _damageAmount = 5;

    private IDealDamage _damageDealer;

    private void Awake()
    {
        _damageDealer = GetComponent<IDealDamage>();

        if (_damageDealer == null)
        {
            Debug.LogWarning("No collision handler component found on " + gameObject.name);
        }
    }

    private void OnEnable()
    {
        if (_damageDealer != null)
        {
            _damageDealer.OnDamage += IDealDamage_OnDamage;
        }
    }

    private void OnDisable()
    {
        if (_damageDealer != null)
        {
            _damageDealer.OnDamage -= IDealDamage_OnDamage;
        }
    }

    private void IDealDamage_OnDamage(GameObject collisionObject)
    {
        SendDamage(collisionObject, _damageAmount);
        Debug.Log($"{_damageAmount} damage sent to {collisionObject.tag} from {gameObject.tag}");
    }

    private void SendDamage(GameObject collisionObject, int damageAmount)
    {
        OnOutputDamage?.Invoke(collisionObject, damageAmount);

    }

}
