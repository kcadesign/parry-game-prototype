using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class HandleOutroScene : MonoBehaviour
{
    protected PlayerControls playerControls;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.IntroOutro.Enable();

        playerControls.IntroOutro.Progress.performed += Progress_performed;
        playerControls.IntroOutro.Progress.canceled += Progress_canceled;
    }

    private void OnDisable()
    {
        playerControls.IntroOutro.Disable();

        playerControls.IntroOutro.Progress.performed -= Progress_performed;
        playerControls.IntroOutro.Progress.canceled -= Progress_canceled;
    }

    private void Progress_performed(InputAction.CallbackContext obj)
    {
        SceneManager.LoadScene(0);
    }

    private void Progress_canceled(InputAction.CallbackContext obj)
    {
        return;
    }

}
