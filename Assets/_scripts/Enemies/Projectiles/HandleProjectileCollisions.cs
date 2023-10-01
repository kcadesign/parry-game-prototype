using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleProjectileCollisions : MonoBehaviour, IDamager, IParryable
{
    public event Action<GameObject> OnDamageCollision;
    public event Action<GameObject, bool> OnDeflect;
    
    private SpriteRenderer _projectileSpriteRenderer;

    public bool _parryActive;
    public bool _blockActive;
    private bool _deflected = false;

    private void Awake()
    {
        _projectileSpriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    protected void OnEnable()
    {
        PlayerParry.OnParryActive += PlayerParry_OnParryActive;
        PlayerBlock.OnBlock += PlayerBlockJump_OnBlock;
    }

    protected void OnDisable()
    {
        PlayerParry.OnParryActive -= PlayerParry_OnParryActive;
        PlayerBlock.OnBlock -= PlayerBlockJump_OnBlock;
    }

    private void PlayerParry_OnParryActive(bool parryPressed)
    {
        _parryActive = parryPressed;
    }

    private void PlayerBlockJump_OnBlock(bool isBlocking)
    {
        _blockActive = isBlocking;
        //Debug.Log($"Block active: {_blockActive}");
    }
    /*
    private void Update()
    {
        Debug.Log($"Block active: {_blockActive}");
    }
    */
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log($"Block active: {_blockActive}");

        if (collision.gameObject.CompareTag("Player"))
        {
            HandleCollisionPlayer(collision);

        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            HandleCollisionEnemy(collision);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void HandleCollisionPlayer(Collision2D collision)
    {
        if (!_deflected)
        {
            if (_parryActive)
            {
                _projectileSpriteRenderer.color = Color.blue;
                _deflected = true;
                OnDeflect?.Invoke(gameObject, _deflected);

                Destroy(gameObject, 3f);
            }
            else if (_blockActive)
            {
                Destroy(gameObject);
            }
            else if (!_parryActive && !_blockActive)
            {
                OnDamageCollision?.Invoke(collision.gameObject);
                Destroy(gameObject);
            }
        }
        else if (_deflected)
        {
            OnDeflect?.Invoke(gameObject, _deflected);
        }
    }

    private void HandleCollisionEnemy(Collision2D collision)
    {
        if (!_deflected)
        {
            return;
        }
        else if (_deflected)
        {
            OnDamageCollision?.Invoke(collision.gameObject);
            Destroy(gameObject);

        }
    }
}
