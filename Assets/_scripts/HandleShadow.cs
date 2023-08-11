using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleShadow : MonoBehaviour
{
    public SpriteRenderer _shadowSpriteRenderer;

    private void Awake()
    {
        //_shadowSpriteRenderer = GetComponent<SpriteRenderer>();

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
        print($"{gameObject.name} is grounded: {grounded}");
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
