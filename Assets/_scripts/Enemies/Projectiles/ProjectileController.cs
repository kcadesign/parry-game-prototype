using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
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

    private void Update()
    {
        
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
            _deflectTargetPosition = _parentObject.transform.position;
            _rigidBody.velocity = Vector2.zero;

            _directionToOrigin = (_deflectTargetPosition - (Vector2)transform.position).normalized;
            _rigidBody.AddForce(_directionToOrigin * _deflectForce, ForceMode2D.Impulse);
        }
    }

}
