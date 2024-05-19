using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class HoldButtonToSkip : MonoBehaviour
{
    protected PlayerControls playerControls;

    private Image BarValueImage;

    private bool _buttonHeld;
    [SerializeField] private float _holdDuration = 0f;
    [SerializeField] private float _requiredHoldTime = 5f;

    private void Awake()
    {
        playerControls = new PlayerControls();
        BarValueImage = GetComponent<Image>();
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
        _buttonHeld = true;
    }

    private void Progress_canceled(InputAction.CallbackContext obj)
    {
        _buttonHeld = false;
    }

    private void Update()
    {
        CheckButtonHeld();
        CheckHoldDuration();
    }

    private void CheckButtonHeld()
    {
        if (_buttonHeld) _holdDuration += Time.deltaTime;
        else  _holdDuration -= Time.deltaTime * 3f;

        GetSetCurrentFill();
    }
    private void CheckHoldDuration()
    {
        if (_holdDuration >= _requiredHoldTime)
        {
            Debug.Log("Button held to execution");
        }
        else if (_holdDuration <= 0)
        {
            _holdDuration = 0;
        }
    }

    private void GetSetCurrentFill()
    {
        float currentFillPercentage = _holdDuration / _requiredHoldTime;
        BarValueImage.fillAmount = currentFillPercentage;
    }

}
