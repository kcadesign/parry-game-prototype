using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlePlatforms : MonoBehaviour
{
    protected PlayerControls playerControls;

    private bool _dropDownPressed = false;
    private bool _grounded;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Gameplay.Enable();

        playerControls.Gameplay.DropDown.performed += DropDown_performed;
        playerControls.Gameplay.DropDown.canceled += DropDown_cancelled;

        CheckPlayerGrounded.OnGrounded += CheckPlayerGrounded_OnGrounded;
    }

    private void OnDisable()
    {
        playerControls.Gameplay.Disable();

        playerControls.Gameplay.DropDown.performed -= DropDown_performed;
        playerControls.Gameplay.DropDown.canceled -= DropDown_cancelled;

        CheckPlayerGrounded.OnGrounded -= CheckPlayerGrounded_OnGrounded;
    }

    private void DropDown_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _dropDownPressed = true;
    }

    private void DropDown_cancelled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _dropDownPressed = false;
    }

    private void CheckPlayerGrounded_OnGrounded(bool grounded)
    {
        _grounded = grounded;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Collision detected : " + collision.gameObject.tag);
        //Debug.Log($"Player grounded: {_grounded}");
        if (collision.gameObject.CompareTag("Platform"))
        {
            if (_dropDownPressed) collision.gameObject.GetComponent<PlatformEffector2D>().rotationalOffset = 180;
            if (_grounded) transform.parent = collision.gameObject.transform;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            if (_dropDownPressed) collision.gameObject.GetComponent<PlatformEffector2D>().rotationalOffset = 180;
            if (_grounded) transform.parent = collision.gameObject.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            collision.gameObject.GetComponent<PlatformEffector2D>().rotationalOffset = 0;
            transform.parent = null;
        }
    }
}
