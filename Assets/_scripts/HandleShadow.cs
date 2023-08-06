using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleShadow : MonoBehaviour
{
    private SpriteRenderer _shadowSpriteRenderer;

    private void Awake()
    {
        _shadowSpriteRenderer = GetComponent<SpriteRenderer>();

    }

    private void OnEnable()
    {
        CheckGrounded.OnGrounded += CheckGrounded_OnGrounded;
    }

    private void OnDisable()
    {
        CheckGrounded.OnGrounded -= CheckGrounded_OnGrounded;

    }

    private void CheckGrounded_OnGrounded(bool grounded)
    {
        if (grounded)
        {
            SetTransparency(0.2f);
        }
        else if (!grounded)
        {
            SetTransparency(0f);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetTransparency(float alphaValue)
    {
        Color currentColor = _shadowSpriteRenderer.color;
        currentColor.a = alphaValue;
        _shadowSpriteRenderer.color = currentColor; 
    }
}
