using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleProjectileCollisions : MonoBehaviour, IDealDamage
{
    public event Action<GameObject> OnDamage;

    public delegate void Deflect(GameObject collisionObject, int damageAmount);
    public static event Deflect OnDeflect;

    //public delegate void ProjectileDamagePlayer();
    //public event ProjectileDamagePlayer OnProjectileDamagePlayer;

    private SpriteRenderer _projectileSpriteRenderer;

    public bool _parryActive;
    public bool _blockActive;
    private bool _deflected = false;
    [SerializeField] private int _enemyDamageAmount = 1;

    private void Awake()
    {
        _projectileSpriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    protected void OnEnable()
    {
        PlayerParry.OnParryActive += PlayerParry_OnParryActive;
        PlayerBlockJump.OnBlock += PlayerBlockJump_OnBlock;
    }

    protected void OnDisable()
    {
        PlayerParry.OnParryActive -= PlayerParry_OnParryActive;
        PlayerBlockJump.OnBlock -= PlayerBlockJump_OnBlock;
    }

    private void PlayerParry_OnParryActive(bool parryPressed)
    {
        _parryActive = parryPressed;
    }

    private void PlayerBlockJump_OnBlock(bool isBlocking)
    {
        _blockActive = isBlocking;
        Debug.Log($"Block active: {_blockActive}");
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
            if (!_deflected)
            {
                HandleCollisionPlayer(collision);
            }
            else if (_deflected)
            {
                return;
            }
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            if (!_deflected)
            {
                return;
            }
            else if (_deflected)
            {
                DamageEnemy(collision);
            }
        }
    }

    private void HandleCollisionPlayer(Collision2D collision)
    {
        if (_parryActive)
        {
            _projectileSpriteRenderer.color = Color.blue;
            _deflected = true;
            Destroy(gameObject, 3f);
        }
        else if (_blockActive)
        {
            Destroy(gameObject);
        }
        else if(!_parryActive && !_blockActive)
        {
            OnDamage?.Invoke(collision.gameObject);
            Destroy(gameObject);
        }
    }

    private void DamageEnemy(Collision2D collision)
    {
        OnDeflect?.Invoke(collision.gameObject, _enemyDamageAmount);
    }
}
