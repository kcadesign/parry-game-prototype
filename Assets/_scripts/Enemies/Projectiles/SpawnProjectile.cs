using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnProjectile : MonoBehaviour
{
    public GameObject Projectile;
    public Transform SpawnPointTransform;
    private Vector2 SpawnPointPosition;

    private bool _parryActive;
    private bool _blockActive;

    private void Awake()
    {
        SpawnPointPosition = SpawnPointTransform.position;
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
        //Debug.Log($"Block active: {_blockActive}");
    }

    private void InstantiateProjectile()
    {
        GameObject projectile = Instantiate(Projectile, SpawnPointPosition, transform.rotation);

        HandleProjectileCollisions projectileScript = projectile.GetComponent<HandleProjectileCollisions>();

        if (projectileScript != null)
        {
            projectileScript._blockActive = _blockActive;
            projectileScript._parryActive = _parryActive;
        }
    }

    public void InvokeProjectile()
    {
        Invoke(nameof(InstantiateProjectile), 0);
    }

    public void CancelInvokeProjectile()
    {
        CancelInvoke(nameof(InstantiateProjectile));
    }
}
