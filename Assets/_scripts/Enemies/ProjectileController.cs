using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private Rigidbody2D _rigidBody;

    //[SerializeField] private float _speed = 5;

    private Vector2 _movementDirection = Vector2.left;

    private bool _deflect = false;

    private void OnEnable()
    {
        PlayerParry.OnParry += PlayerParry_OnParry;
    }

    private void OnDisable()
    {
        PlayerParry.OnParry -= PlayerParry_OnParry;

    }

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void PlayerParry_OnParry()
    {
        _deflect = true;
    }

    void Start()
    {
        _rigidBody.AddForce(_movementDirection, ForceMode2D.Impulse);
    }

    void Update()
    {
        //this.transform.Translate(_movementDirection * _speed * Time.deltaTime);
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        print(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Player") && _deflect)
        {
            //_movementDirection = Vector2.zero;
            Destroy(gameObject,3f);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
}
