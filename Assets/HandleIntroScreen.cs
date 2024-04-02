using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class HandleIntroScreen : MonoBehaviour
{
    protected PlayerControls playerControls;

    private int _nextScene;

    private void Awake()
    {
        playerControls = new PlayerControls();
        _nextScene = SceneManager.GetActiveScene().buildIndex + 1;
    }

    private void OnEnable()
    {
        playerControls.Intro.Enable();

        playerControls.Intro.NextScene.performed += NextScene_performed;
        playerControls.Intro.NextScene.canceled += NextScene_canceled;
    }

    private void OnDisable()
    {
        playerControls.Intro.Disable();

        playerControls.Intro.NextScene.performed -= NextScene_performed;
        playerControls.Intro.NextScene.canceled -= NextScene_canceled;
    }

    private void NextScene_performed(InputAction.CallbackContext obj)
    {
        Debug.Log($"Loading default scene: {_nextScene}");
        SceneManager.LoadScene(_nextScene);
    }    
    
    private void NextScene_canceled(InputAction.CallbackContext obj)
    {
        return;
    }

}
