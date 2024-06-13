using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class HoldButtonToSkip : MonoBehaviour
{
    protected PlayerControls playerControls;

    public static event Action OnButtonHeldToExecution;

    private Image BarValueImage;

    private bool _buttonHeld;
    [SerializeField] private float _holdDuration = 0f;
    [SerializeField] private float _requiredHoldTime = 5f;
    private bool _buttonHeldToExecution;

    private void Awake()
    {
        playerControls = new PlayerControls();
        BarValueImage = GetComponent<Image>();
    }

    private void Start()
    {
        _buttonHeldToExecution = false;
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
        if(_buttonHeldToExecution) return;
        else if ((!_buttonHeldToExecution))
        {
            CheckButtonHeld();
            CheckHoldDuration();
        }
    }

    private void CheckButtonHeld()
    {
        //Debug.Log("Button held: " + _buttonHeld);
        if (_buttonHeld && _holdDuration < _requiredHoldTime) _holdDuration += Time.unscaledDeltaTime;
        else  _holdDuration -= Time.unscaledDeltaTime * 3f;

        GetSetCurrentFill();
    }

    private void CheckHoldDuration()
    {
        if (_holdDuration >= _requiredHoldTime)
        {
            //Debug.Log("Button held to execution");
            OnButtonHeldToExecution?.Invoke();
            _buttonHeldToExecution = true;
            //_holdDuration = _requiredHoldTime;
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
