using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System;

public class ButtonSelectionHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
    public static event Action OnButtonSelected;
    public static event Action OnButtonpressed;

    public ButtonManager ButtonManager;
    protected PlayerControls playerControls;

    public GameObject ButtonSelector;
    public TextMeshProUGUI ButtonText;
    private Vector3 _buttonOriginalPosition;
    private float _buttonMoveAmount = 10;

    [SerializeField] private float _moveTime = 0.1f;
    [Range(0, 2), SerializeField] private float _scaleAmount = 1.1f;

    private Vector3 _originalScale;

    private bool _buttonSelected = false;
    private bool _executePressed = false;

    private void Awake()
    {
        playerControls = new PlayerControls();
        _originalScale = transform.localScale;
        _buttonOriginalPosition = ButtonText.transform.localPosition;
        Debug.Log("ButtonPressedPosition: " + _buttonOriginalPosition);
    }

    private void OnEnable()
    {
        playerControls.Menus.Enable();
        playerControls.Menus.Execute.performed += Execute_performed;
        playerControls.Menus.Execute.canceled += Execute_canceled;
    }

    private void OnDisable()
    {
        playerControls.Menus.Disable();
        playerControls.Menus.Execute.performed -= Execute_performed;
        playerControls.Menus.Execute.canceled -= Execute_canceled;

        // Ensure the button scale and text position are reset when deactivated
        transform.localScale = _originalScale;
        ButtonText.transform.localPosition = _buttonOriginalPosition;
    }

    private void Execute_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (_buttonSelected)
        {
            OnButtonPress();
        }
        StartCoroutine(CancelExecute());
    }

    private void Execute_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        // No action needed here as the cancel will be handled by the coroutine
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        eventData.selectedObject = gameObject;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        eventData.selectedObject = null;
    }

    public void OnSelect(BaseEventData eventData)
    {
        OnButtonSelected?.Invoke();
        _buttonSelected = true;

        LeanTween.scale(gameObject, _originalScale * _scaleAmount, _moveTime).setEase(LeanTweenType.easeInOutExpo).setIgnoreTimeScale(true);

        ButtonManager.LastSelectedButton = gameObject;
        for (int i = 0; i < ButtonManager.Buttons.Length; i++)
        {
            if (ButtonManager.Buttons[i] == gameObject)
            {
                ButtonManager.LastSelectedIndex = i;
                return;
            }
        }
        ButtonSelector.SetActive(true);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        _buttonSelected = false;
        LeanTween.scale(gameObject, _originalScale, _moveTime).setEase(LeanTweenType.easeInOutExpo).setIgnoreTimeScale(true);
        ButtonSelector.SetActive(false);
    }

    public void OnPointerDown()
    {
        OnButtonPress();
    }

    public void OnPointerUp()
    {
        OnButtonRelease();
    }

    private void OnButtonPress()
    {
        MoveTextWithButton(new Vector3(transform.position.x, transform.position.y, transform.position.z));
        OnButtonpressed?.Invoke();
    }

    private void OnButtonRelease()
    {
        MoveTextWithButton(new Vector3(transform.position.x, transform.position.y + _buttonMoveAmount, transform.position.z));
    }

    public void MoveTextWithButton(Vector3 buttonPosition)
    {
        ButtonText.transform.position = buttonPosition;
    }

    private IEnumerator CancelExecute()
    {
        yield return new WaitForSeconds(0.1f);
        if (_buttonSelected) // Ensure the button is still selected before resetting
        {
            OnButtonRelease();
        }
    }
}
