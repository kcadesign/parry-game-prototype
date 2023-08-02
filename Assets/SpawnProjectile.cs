using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnProjectile : MonoBehaviour
{
    public GameObject Projectile;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(InstantiateProjectile), 0, 2);
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
