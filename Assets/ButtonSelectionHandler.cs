using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ButtonSelectionHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
    public ButtonManager ButtonManager;
    protected PlayerControls playerControls;

    public GameObject ButtonSelector;
    public TextMeshProUGUI ButtonText;
    private Vector3 _buttonTextOriginalPosition;

    [SerializeField] private float _moveTime = 0.1f;
    [Range(0, 2), SerializeField] private float _scaleAmount = 1.1f;

    private Vector3 _startPosition;
    private Vector3 _originalScale;

    private bool _buttonSelected = false;

    private void Awake()
    {
        playerControls = new PlayerControls();

        _startPosition = transform.position;
        _originalScale = transform.localScale;

        _buttonTextOriginalPosition = ButtonText.transform.position;
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
    }

    private void Execute_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Debug.Log("Execute performed");
        if (_buttonSelected)
        {
            MoveTextWithButton(new Vector3(transform.position.x, transform.position.y, transform.position.z));
        }
    }

    private void Execute_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Debug.Log("Execute canceled");
        MoveTextWithButton(_buttonTextOriginalPosition);
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
        Debug.Log($"{gameObject.name} selected");
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
        //Debug.Log("Pointer Down");
        MoveTextWithButton(new Vector3(transform.position.x, transform.position.y, transform.position.z));
    }

    public void OnPointerUp()
    {
        //Debug.Log("Pointer Up");
        MoveTextWithButton(_buttonTextOriginalPosition);
     }

    public void MoveTextWithButton(Vector3 buttonPosition)
    {
        ButtonText.transform.position = buttonPosition;
    }

}
