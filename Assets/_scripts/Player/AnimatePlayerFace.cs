using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimatePlayerFace : MonoBehaviour
{
    protected PlayerControls playerControls;

    private Animator _faceAnimator;

    private Vector2 _movementAxis;

    private void Awake()
    {
        playerControls = new PlayerControls();

        _faceAnimator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        playerControls.Gameplay.Enable();

        playerControls.Gameplay.Rolling.performed += Rolling_performed;
        playerControls.Gameplay.Rolling.canceled += Rolling_canceled;
    }

    private void OnDisable()
    {
        playerControls.Gameplay.Disable();

        playerControls.Gameplay.Rolling.performed -= Rolling_performed;
        playerControls.Gameplay.Rolling.canceled -= Rolling_canceled;
    }

    private void Rolling_performed(InputAction.CallbackContext value)
    {
        _movementAxis = value.ReadValue<Vector2>();
    }

    private void Rolling_canceled(InputAction.CallbackContext value)
    {
        _movementAxis = Vector2.zero;
    }

    void Update()
    {
        if (MovingLeft())
        {
            _faceAnimator.SetBool("RollingLeft", true);
        }
        else
        {
            _faceAnimator.SetBool("RollingLeft", false);
        }

        if (MovingRight())
        {
            _faceAnimator.SetBool("RollingRight", true);
        }
        else
        {
            _faceAnimator.SetBool("RollingRight", false);
        }
    }

    private bool MovingLeft()
    {
        if(_movementAxis.x < -0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool MovingRight()
    {
        if (_movementAxis.x > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
