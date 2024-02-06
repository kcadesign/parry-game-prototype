using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnProjectile : MonoBehaviour
{
    public GameObject Projectile;
    public Transform SpawnPointTransform;
    private Vector2 SpawnPointPosition;
    [SerializeField] private Vector2 _projectileMovementDirection;
    [SerializeField] private float _speed = 5;

/*    private void Awake()
    {

    }
*/
    private void InstantiateProjectile()
    {
        SpawnPointPosition = SpawnPointTransform.position;

        GameObject projectile = Instantiate(Projectile, SpawnPointPosition, transform.rotation, gameObject.transform);
        Rigidbody2D projectileRigidbody = projectile.GetComponent<Rigidbody2D>();

        if(projectileRigidbody != null)
        {
            projectileRigidbody.AddForce(_projectileMovementDirection * _speed, ForceMode2D.Impulse);
        }
    }

    public void InvokeProjectile() => Invoke(nameof(InstantiateProjectile), 0);
    public void CancelInvokeProjectile() => CancelInvoke(nameof(InstantiateProjectile));
    
}
