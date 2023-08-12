using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleEnemyShadow : MonoBehaviour
{
    private SpriteRenderer _shadowSpriteRenderer;

    private void Awake()
    {
        _shadowSpriteRenderer = GetComponent<SpriteRenderer>();
    }


    // Update is called once per frame
    void Update()
    {
        if (CheckEnemyGrounded.IsGrounded)
        {
            SetTransparency(0.2f);
            _shadowSpriteRenderer.enabled = true;

        }
        else if (!CheckEnemyGrounded.IsGrounded)
        {
            SetTransparency(0f);
            _shadowSpriteRenderer.enabled = false;

        }
    }

    private void SetTransparency(float alphaValue)
    {
        Color currentColor = _shadowSpriteRenderer.color;
        currentColor.a = alphaValue;
        _shadowSpriteRenderer.color = currentColor;
    }
}
