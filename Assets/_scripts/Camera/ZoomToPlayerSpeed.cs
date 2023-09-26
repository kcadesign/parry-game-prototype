using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ZoomToPlayerSpeed : MonoBehaviour
{
    private CinemachineVirtualCamera _virtualCamera;

    [SerializeField] private float _targetPlayerVelocity;

    [SerializeField] private float _zoomSpeed = 0.1f;

    [SerializeField] private float _startingOrthoSize;
    [SerializeField] private float _maxCameraOrthoSize;
    private float _targetOrthoSize;

    private void Awake()
    {
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
        _virtualCamera.m_Lens.OrthographicSize = _startingOrthoSize;
    }

    private void OnEnable()
    {
        PlayerMove.OnPlayerMoveInput += PlayerMove_OnPlayerMoveInput;
    }

    private void OnDisable()
    {
        PlayerMove.OnPlayerMoveInput -= PlayerMove_OnPlayerMoveInput;
    }

    private void PlayerMove_OnPlayerMoveInput(Vector2 playerVelocity)
    {
        if (playerVelocity.x >= _targetPlayerVelocity)
        {
            _targetOrthoSize = _maxCameraOrthoSize;
        }
        else if (playerVelocity.x <= -_targetPlayerVelocity)
        {
            _targetOrthoSize = _maxCameraOrthoSize;
        }
        else
        {
            _targetOrthoSize = _startingOrthoSize;
        }
        _virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(_virtualCamera.m_Lens.OrthographicSize, _targetOrthoSize, _zoomSpeed * Time.deltaTime);
    }

}
