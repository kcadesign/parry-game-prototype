using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollisions : HandleEnemyCollisions
{
    public delegate void Deflect(bool deflected);
    public static event Deflect OnDeflect;

    private SpriteRenderer _projectileSpriteRenderer;

    private bool _deflected = false;

    private void Awake()
    {
        _projectileSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);

        print(collision.gameObject.tag);

        if (collision.gameObject.CompareTag("Player") && _isParrying)
        {
            _projectileSpriteRenderer.color = Color.blue;

            _deflected = true;
            OnDeflect?.Invoke(_deflected);

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
            
            Destroy(gameObject);
            Destroy(collision.transform.parent.gameObject);
        }
    }
}
