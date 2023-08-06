using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleCollision : MonoBehaviour
{
    private bool _deflected;

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
        _deflected = deflected;
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
        if (collision.gameObject.CompareTag("Projectile") && _deflected)
        {
            Destroy(transform.parent.gameObject);
        }
    }


}
