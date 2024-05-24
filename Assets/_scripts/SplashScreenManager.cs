using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreenManager : MonoBehaviour
{
    protected PlayerControls playerControls;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.IntroOutro.Enable();

        playerControls.IntroOutro.Start.performed += Start_performed;
        playerControls.IntroOutro.Start.canceled += Start_canceled;
    }

    private void OnDisable()
    {
        playerControls.IntroOutro.Disable();

        playerControls.IntroOutro.Start.performed -= Start_performed;
        playerControls.IntroOutro.Start.canceled -= Start_canceled;
    }

    private void Start_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Debug.Log("Start requested on splash screen");
        SceneTransitionManager.TransitionManagerInstance.LoadScene("Menu-Start", "WipePinkTransition");
    }

    private void Start_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        return;
    }
}
