using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleDamage : MonoBehaviour
{
    private void OnEnable()
    {
        HandleDamageOut.OnOutputDamage += HandleDamageOut_OnOutputDamage;
    }

    private void OnDisable()
    {
        HandleDamageOut.OnOutputDamage -= HandleDamageOut_OnOutputDamage;
    }

    private void HandleDamageOut_OnOutputDamage(GameObject damager, GameObject collisionObject, int damageAmount)
    {
        if(collisionObject == gameObject)
        {

        }
    }

}
