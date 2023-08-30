using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnProjectile : MonoBehaviour
{
    public GameObject Projectile;
    public Transform SpawnPointTransform;
    private Vector2 SpawnPointPosition;

    [SerializeField] private float _spawnDelay;
    [SerializeField] private float _spawnRate;

    private void Awake()
    {
        SpawnPointPosition = SpawnPointTransform.position;
    }
    
    private void OnEnable()
    {
        InvokeRepeating(nameof(InstantiateProjectile), _spawnDelay, _spawnRate);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(InstantiateProjectile));
    }

    void Start()
    {

    }
    
    private void InstantiateProjectile()
    {
        Instantiate(Projectile, SpawnPointPosition, transform.rotation);
    }
}
