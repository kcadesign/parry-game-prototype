using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlePlayerMaterials : MonoBehaviour
{
    public Collider2D _playerCollider;
    public PhysicsMaterial2D DefaultPlayerMaterial;
    public PhysicsMaterial2D BouncyMaterial;

    private bool _parryActive;
    private bool _blockActive;

    private void OnEnable()
    {
        PlayerParry.OnParryActive += PlayerParry_OnParryActive;
        PlayerBlock.OnBlock += PlayerBlock_OnBlock;
    }

    private void OnDisable()
    {
        PlayerParry.OnParryActive -= PlayerParry_OnParryActive;
        PlayerBlock.OnBlock -= PlayerBlock_OnBlock;
    }

    private void PlayerParry_OnParryActive(bool parryPressed)
    {
        _parryActive = parryPressed;
    }

    private void PlayerBlock_OnBlock(bool isBlocking)
    {
        _blockActive = isBlocking;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_parryActive)
        {
            _playerCollider.sharedMaterial = BouncyMaterial;
        }
        else if (_blockActive)
        {
            _playerCollider.sharedMaterial = null;

        }
        else if (!_parryActive && !_blockActive)
        {
            _playerCollider.sharedMaterial = DefaultPlayerMaterial;
        }
    }
}
