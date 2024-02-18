using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public static event Action<GameObject> OnProjectileShoot;
    public static event Action<GameObject> OnProjectileDeflect;
    private Rigidbody2D _rigidBody;

    private GameObject _parentObject;

    [SerializeField] private float _deflectForce;

    private Vector2 _deflectTargetPosition;
    private Vector2 _directionToOrigin;

    private IParryable _parryable;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _parryable = GetComponent<IParryable>();
        _parentObject = gameObject.transform.parent.gameObject;

        gameObject.transform.parent = null;
    }

    private void OnEnable()
    {
        if (_parryable != null)
        {
            _parryable.OnDeflect += Parryable_OnDeflect;
        }
    }

    private void OnDisable()
    {
        if (_parryable != null)
        {
            _parryable.OnDeflect -= Parryable_OnDeflect;
        }
    }

    private void Update()
    {
        
    }

    void Start()
    {
        _deflectTargetPosition = _parentObject.transform.position;

        OnProjectileShoot?.Invoke(gameObject);

        //_rigidBody.AddForce(_movementDirection * _speed, ForceMode2D.Impulse);
        Destroy(gameObject, 5f);
    }    
    
    private void Parryable_OnDeflect(GameObject parriedObject, bool deflected)
    {
        if (parriedObject == gameObject && deflected)
        {
            OnProjectileDeflect?.Invoke(gameObject);

            // if the deflect target position isnt null, register new target position and deflect towards it
            if (_parentObject != null)
            {
                _deflectTargetPosition = _parentObject.transform.position;
                _rigidBody.velocity = Vector2.zero;

                _directionToOrigin = (_deflectTargetPosition - (Vector2)transform.position).normalized;
                _rigidBody.AddForce(_directionToOrigin * _deflectForce, ForceMode2D.Impulse);
            }
            else 
            {
                // Use position set in Start if no parent object is found
                _directionToOrigin = (_deflectTargetPosition - (Vector2)transform.position).normalized;

                _rigidBody.velocity = Vector2.zero;
                _rigidBody.AddForce(_directionToOrigin * _deflectForce, ForceMode2D.Impulse);
            }
        }
    }

}
