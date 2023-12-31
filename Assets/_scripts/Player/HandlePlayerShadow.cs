using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlePlayerShadow : MonoBehaviour
{
    private SpriteRenderer _shadowSpriteRenderer;

    private void Awake()
    {
        _shadowSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        CheckPlayerGrounded.OnGrounded += CheckGrounded_OnGrounded;
    }

    private void OnDisable()
    {
        CheckPlayerGrounded.OnGrounded -= CheckGrounded_OnGrounded;
    }

    private void CheckGrounded_OnGrounded(bool grounded)
    {
        //print($"{gameObject.name} is grounded: {grounded}");
        if (grounded)
        {
            SetTransparency(0.2f);
            _shadowSpriteRenderer.enabled = true;
        }
        else if (!grounded)
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
