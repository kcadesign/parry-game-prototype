using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private Rigidbody2D _rigidBody;

    [SerializeField] private float _speed = 5;
    private Vector2 _movementDirection = Vector2.left;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        _rigidBody.AddForce(_movementDirection * _speed, ForceMode2D.Impulse);
        Destroy(gameObject, 5f);
    }
    
}
