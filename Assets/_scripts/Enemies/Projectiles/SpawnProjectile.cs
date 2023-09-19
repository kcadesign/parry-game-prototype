using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnProjectile : MonoBehaviour
{
    public GameObject Projectile;
    public Transform SpawnPointTransform;
    private Vector2 SpawnPointPosition;

    private void Awake()
    {
        SpawnPointPosition = SpawnPointTransform.position;
    }
           
    private void InstantiateProjectile()
    {
        Instantiate(Projectile, SpawnPointPosition, transform.rotation);
    }

    public void InvokeProjectile()
    {
        Invoke(nameof(InstantiateProjectile), 0);
    }

    public void CancelInvokeProjectile()
    {
        CancelInvoke(nameof(InstantiateProjectile));
    }
}
