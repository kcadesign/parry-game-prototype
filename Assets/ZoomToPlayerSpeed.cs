using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ZoomToPlayerSpeed : MonoBehaviour
{
    private CinemachineVirtualCamera _virtualCamera;
    private Vector2 _playerVelocity;

    [SerializeField] private float _targetPlayerVelocity;

    [SerializeField] private float _zoomSpeed = 0.1f;

    private float _startingOrthoSize;
    private float _targetOrthoSize;
    [SerializeField] private float _maxCameraOrthoSize;

    private void Awake()
    {
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
        _startingOrthoSize = _virtualCamera.m_Lens.OrthographicSize;
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
        _playerVelocity = playerVelocity;

        if (_playerVelocity.x > _targetPlayerVelocity)
        {
            _targetOrthoSize = _maxCameraOrthoSize;
        }
        else if (_playerVelocity.x < -_targetPlayerVelocity)
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
