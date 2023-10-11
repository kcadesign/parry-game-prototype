using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private Rigidbody2D _rigidBody;

    //[SerializeField] private float _speed = 5;
    //private Vector2 _movementDirection = Vector2.left;

    [SerializeField] private float _deflectForce;

    private Vector2 _originPosition;
    private Vector2 _directionToOrigin;

    private IParryable _parryable;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _parryable = GetComponent<IParryable>();
        _originPosition = gameObject.transform.position;
    }

    private void OnEnable()
    {
        if (_parryable != null)
        {
            _parryable.OnDeflect += _parryable_OnDeflect;
        }
    }

    private void OnDisable()
    {
        if (_parryable != null)
        {
            _parryable.OnDeflect -= _parryable_OnDeflect;
        }
    }

    void Start()
    {
        //_rigidBody.AddForce(_movementDirection * _speed, ForceMode2D.Impulse);
        Destroy(gameObject, 5f);
    }    
    
    private void _parryable_OnDeflect(GameObject parriedObject, bool deflected)
    {
        if (parriedObject == gameObject && deflected)
        {
            _rigidBody.velocity = Vector2.zero;

            _directionToOrigin = (_originPosition - (Vector2)transform.position).normalized;
            _rigidBody.AddForce(_directionToOrigin * _deflectForce, ForceMode2D.Impulse);
        }
    }

}
