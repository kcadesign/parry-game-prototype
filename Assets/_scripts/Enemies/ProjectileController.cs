using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private float _speed = 5;

    private Vector2 _movementDirection = Vector2.left;

    private bool _deflect = false;

    private void OnEnable()
    {
        PlayerParry.OnParry += PlayerParry_OnParry;
    }

    private void PlayerParry_OnParry()
    {
        _deflect = true;
    }

    void Start()
    {
        
    }

    void Update()
    {
        this.transform.Translate(_movementDirection * _speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print(collision.gameObject);
        if (collision.gameObject.CompareTag("Player") && _deflect)
        {
            _movementDirection = Vector2.right;
            Destroy(gameObject,3f);
        }
        else if(collision.gameObject.CompareTag("Player") && !_deflect)
        {
            Destroy(gameObject);
        }
    }
}
