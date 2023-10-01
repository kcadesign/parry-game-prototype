using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleCollisions : MonoBehaviour
{
    /*
    public delegate void CollisionWithDamager(GameObject collidedObject);
    public static event CollisionWithDamager OnCollisionWithDamager;

    public delegate void CollisionWithDamageable(GameObject collidedObject);
    public static event CollisionWithDamageable OnCollisionWithDamageable;

    public delegate void CollisionWithObject(GameObject collidedObject);
    public static event CollisionWithObject OnCollisionWithObject;
    */
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Collision detected");
        IDamager damager = collision.gameObject.GetComponent<IDamager>();
        if (damager != null)
        {
            //Debug.Log("Collision with IDamager detected");

            HandleCollisionWithDamager(collision);
        }

        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            HandleCollisionWithDamageable(collision);
        }

        if (damager == null && damageable == null)
        {
            HandleCollisionWithStandard(collision);
        }
    }

    protected void OnCollisionStay2D(Collision2D collision)
    {

    }

    protected void OnCollisionExit2D(Collision2D collision)
    {

    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<IDamager>() != null)
        {
            HandleTriggerEnterDamager(collision);
        }
        if (collision.gameObject.GetComponent<IDamageable>() != null)
        {
            HandleTriggerEnterDamageable(collision);
        }
        if (collision.gameObject.GetComponent<IDamager>() == null && collision.gameObject.GetComponent<IDamageable>() == null)
        {
            HandleTriggerEnterStandard(collision);
        }
    }

    protected void OnTriggerStay2D(Collider2D collision)
    {

    }

    protected void OnTriggerExit2D(Collider2D collision)
    {

    }

    protected virtual void HandleCollisionWithStandard(Collision2D collision) { }

    protected virtual void HandleCollisionWithDamageable(Collision2D collision) { }

    protected virtual void HandleCollisionWithDamager(Collision2D collision) { }

    protected virtual void HandleTriggerEnterStandard(Collider2D collision) { }

    protected virtual void HandleTriggerEnterDamageable(Collider2D collision) { }

    protected virtual void HandleTriggerEnterDamager(Collider2D collision) { }
}
