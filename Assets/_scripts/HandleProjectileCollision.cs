using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleProjectileCollision : MonoBehaviour
{
    private bool _takeDamage;

    private void OnEnable()
    {
        ProjectileController.OnDeflect += ProjectileController_OnDeflect;
    }

    private void OnDisable()
    {
        ProjectileController.OnDeflect -= ProjectileController_OnDeflect;
    }

    private void Awake()
    {

    }

    private void ProjectileController_OnDeflect(bool deflected)
    {
        _takeDamage = deflected;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_takeDamage)
        {
            Destroy(transform.parent.gameObject);
        }

    }


}
