using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlePlatforms : MonoBehaviour
{
    protected PlayerControls playerControls;

    private bool _dropDownPressed = false;
    private bool _dropping = false;
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
        if (!_dropping && !_dropDownPressed)
        {
            _dropDownPressed = true;
        }
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
        if (collision.gameObject.CompareTag("Platform"))
        {
/*            if (_grounded)
            {
                transform.parent = collision.gameObject.transform;
            }
*/
            if (_dropDownPressed && !_dropping)
            {
                StartCoroutine(DropThroughPlatform(collision.gameObject));
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
/*            if (_grounded)
            {
                transform.parent = collision.gameObject.transform;
            }
*/
            if (_dropDownPressed && !_dropping)
            {
                StartCoroutine(DropThroughPlatform(collision.gameObject));
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            if (collision.gameObject.TryGetComponent(out PlatformEffector2D effector))
            {
                effector.rotationalOffset = 0;
            }
            _dropping = false;

/*            if (transform.parent != null)
            {
                transform.parent = null;
            }
*/        }
    }

    private IEnumerator DropThroughPlatform(GameObject platform)
    {
        _dropping = true;

        // Try to get the PlatformEffector2D component
        if (platform.TryGetComponent(out PlatformEffector2D effector))
        {
            // Set the platform effector to allow dropping through
            effector.rotationalOffset = 180;

            // Wait for a short duration to allow the player to drop through the platform
            yield return new WaitForSeconds(0.5f);

            // Reset the platform effector
            effector.rotationalOffset = 0;
        }

        _dropping = false;
        _dropDownPressed = false;
    }
}
