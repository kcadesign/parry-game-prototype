using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private Rigidbody2D _rigidBody;

    [SerializeField] private float _speed = 5;
    private Vector2 _movementDirection = Vector2.left;

    [SerializeField] private float _deflectForce;

    private Vector2 _originPosition;
    private Vector2 _originDirection;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _originPosition = gameObject.transform.position;
    }

    private void OnEnable()
    {
        HandleProjectileCollisions.OnDeflect += HandleProjectileCollisions_OnDeflect;
    }

    private void OnDisable()
    {
        HandleProjectileCollisions.OnDeflect -= HandleProjectileCollisions_OnDeflect;

    }

    void Start()
    {
        _rigidBody.AddForce(_movementDirection * _speed, ForceMode2D.Impulse);
        Destroy(gameObject, 5f);
    }

    private void HandleProjectileCollisions_OnDeflect(GameObject projectile, bool deflected)
    {
        if (projectile == gameObject && deflected)
        {
            _rigidBody.velocity = Vector2.zero;

            _originDirection = (_originPosition - (Vector2)transform.position).normalized;
            _rigidBody.AddForce(_originDirection * _deflectForce, ForceMode2D.Impulse);
        }
    }

}
