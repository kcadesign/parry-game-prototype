using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    private Transform[] childTransformArray;
    public HandleEnemyHealth EnemyHealth;

    private void Start()
    {
        childTransformArray = GetComponentsInChildren<Transform>();

        if (childTransformArray.Length > 1)
        {
            int totalChildren = childTransformArray.Length - 1; // Exclude the parent.
            int totalDamage = EnemyHealth.MaxHealth;

            int damagePerChild = (totalDamage + totalChildren - 1) / totalChildren;

            foreach (Transform childTransform in childTransformArray)
            {
                if (childTransform != transform) // Exclude the parent.
                {
                    HandleDamageOut childDamageOut = childTransform.GetComponent<HandleDamageOut>();

                    if (childDamageOut != null)
                    {
                        childDamageOut._damageAmount = damagePerChild;
                    }
                }
            }
        }
    }
}
