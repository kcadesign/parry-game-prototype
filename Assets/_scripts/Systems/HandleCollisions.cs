using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleCollisions : MonoBehaviour
{
    [SerializeField] protected bool _collisionRequired;

    public enum CollisionType
    {
        Player,
        EnemyBody,
        Projectile,
        HurtBox,
        Environment
    }

    protected CollisionType _currentCollision;

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        DefineCollidedObject(collision.gameObject);
    }

    protected void OnCollisionStay2D(Collision2D collision)
    {

    }

    protected void OnCollisionExit2D(Collision2D collision)
    {

    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        DefineCollidedObject(collision.gameObject);
    }

    protected void OnTriggerStay2D(Collider2D collision)
    {

    }

    protected void OnTriggerExit2D(Collider2D collision)
    {

    }

    protected void DefineCollidedObject(GameObject collidedObject)
    {
        if (collidedObject.CompareTag("Player")) _currentCollision = CollisionType.Player;
        else if (collidedObject.CompareTag("Enemy")) _currentCollision = CollisionType.EnemyBody;
        else if (collidedObject.CompareTag("Projectile")) _currentCollision = CollisionType.HurtBox;
        else if (collidedObject.CompareTag("HurtBox")) _currentCollision = CollisionType.Projectile;
        else _currentCollision = CollisionType.Environment;

        switch (_currentCollision)
        {
            case CollisionType.Player:
                HandleCollisionWithPlayer(collidedObject);
                break;
            case CollisionType.EnemyBody:
                HandleCollisionWithEnemyBody(collidedObject);
                break;
            case CollisionType.Projectile:
                HandleCollisionWithProjectile(collidedObject);
                break;
            case CollisionType.HurtBox:
                HandleCollisionWithHurtBox(collidedObject);
                break;
            case CollisionType.Environment:
                HandleCollisionWithEnvironment(collidedObject);
                break;
            default:
                HandleCollisionWithEnvironment(collidedObject);
                break;
        }
        //Debug.Log($"{gameObject.tag} collided with {collidedObject.tag}");
    }

    protected virtual void HandleCollisionWithPlayer(GameObject collidedObject) { }
    protected virtual void HandleCollisionWithEnemyBody(GameObject collidedObject) { }
    protected virtual void HandleCollisionWithProjectile(GameObject collidedObject) { }
    protected virtual void HandleCollisionWithHurtBox(GameObject collidedObject) { }
    protected virtual void HandleCollisionWithEnvironment(GameObject collidedObject) { }

}
