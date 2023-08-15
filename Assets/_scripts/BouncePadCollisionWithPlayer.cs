using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePadCollisionWithPlayer : MonoBehaviour
{
    public CircleCollider2D BouncyCollider;
    public CircleCollider2D DefaultCollider;

    private bool _isParrying;

    private void Awake()
    {

    }

    private void OnEnable()
    {
        PlayerParry.OnParryActive += PlayerParry_OnParryActive;
    }

    private void OnDisable()
    {
        PlayerParry.OnParryActive -= PlayerParry_OnParryActive;

    }
    private void PlayerParry_OnParryActive(bool parryPressed)
    {
        _isParrying = parryPressed;
    }

    void Start()
    {

    }

    void FixedUpdate()
    {
        HandlePlayerInteraction();
    }

    private void HandlePlayerInteraction()
    {
        if (!_isParrying)
        {
            DefaultCollider.enabled = true;
            BouncyCollider.enabled = false;
        }
        else
        {
            DefaultCollider.enabled = false;
            BouncyCollider.enabled = true;
        }
    }
}
