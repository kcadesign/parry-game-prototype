using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlePlatforms : MonoBehaviour
{
    protected PlayerControls playerControls;

    private bool _dropDownPressed = false;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Gameplay.Enable();

        playerControls.Gameplay.DropDown.performed += DropDown_performed;
        playerControls.Gameplay.DropDown.canceled += DropDown_cancelled;
    }

    private void OnDisable()
    {
        playerControls.Gameplay.Disable();

        playerControls.Gameplay.DropDown.performed -= DropDown_performed;
        playerControls.Gameplay.DropDown.canceled -= DropDown_cancelled;
    }

    private void DropDown_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _dropDownPressed = true;
    }

    private void DropDown_cancelled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _dropDownPressed = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform") && _dropDownPressed)
        {
            collision.gameObject.GetComponent<PlatformEffector2D>().rotationalOffset = 180;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform") && _dropDownPressed)
        {
            collision.gameObject.GetComponent<PlatformEffector2D>().rotationalOffset = 180;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            collision.gameObject.GetComponent<PlatformEffector2D>().rotationalOffset = 0;
        }
    }
}
