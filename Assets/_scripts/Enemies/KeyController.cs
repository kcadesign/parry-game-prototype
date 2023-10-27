using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    private Transform[] childTransforms;
    public HandleEnemyHealth EnemyHealth;

    private void Start()
    {
        childTransforms = GetComponentsInChildren<Transform>();

        if (childTransforms.Length > 1)
        {
            int totalChildren = childTransforms.Length - 1; // Exclude the parent.
            int totalDamage = EnemyHealth.MaxHealth;

            int damagePerChild = (totalDamage + totalChildren - 1) / totalChildren;

            foreach (Transform childTransform in childTransforms)
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
