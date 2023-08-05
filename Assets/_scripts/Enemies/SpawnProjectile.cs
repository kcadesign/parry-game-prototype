using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnProjectile : MonoBehaviour
{
    public GameObject Projectile;

    [SerializeField] private float _spawnDelay;
    [SerializeField] private float _spawnRate;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(InstantiateProjectile), _spawnDelay, _spawnRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InstantiateProjectile()
    {
        Instantiate(Projectile, transform.position, transform.rotation, transform);

    }
}
