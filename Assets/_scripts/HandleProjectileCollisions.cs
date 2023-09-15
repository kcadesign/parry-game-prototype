using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleProjectileCollisions : HandleEnemyCollisions
{
    public delegate void Deflect(GameObject collisionObject, int DamageAmount);
    public static event Deflect OnDeflect;

    private SpriteRenderer _projectileSpriteRenderer;

    private bool _deflected = false;
    [SerializeField] private int _damageAmount = 5;

    private void Awake()
    {
        _projectileSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);

        Debug.Log($"Projectile collided with {collision.gameObject.tag}");

        if (collision.gameObject.CompareTag("Player") && _isParrying)
        {
            _projectileSpriteRenderer.color = Color.blue;

            _deflected = true;
            OnDeflect?.Invoke(collision.gameObject, _damageAmount);

            Destroy(gameObject, 3f);

        }
        else if (collision.gameObject.CompareTag("Player") && !_isParrying)
        {
            _deflected = false;

            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Enemy") && !_deflected)
        {
            return;
        }
        else if (collision.gameObject.CompareTag("Enemy") && _deflected)
        {
            OnDeflect?.Invoke(collision.gameObject, _damageAmount);
        }
    }
}
