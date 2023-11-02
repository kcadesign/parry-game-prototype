using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowOnTriggerEnter : MonoBehaviour
{
    public static event Action<bool> OnFollow;

    [Header("Position Parameters")]
    private GameObject _objectToFollow;
    public Vector3 Offset;
    private bool _onPlayerLeft;
    private bool _onPlayerRight;
    private bool _isFollowing;

    [Header("Position Constraints")]
    public bool ConstrainXPosition = false;
    public bool ConstrainYPosition = false;
    public bool ConstrainZPosition = false;

    [Header("Rotation Constraints")]
    public bool ConstrainXRotation = false;
    public bool ConstrainYRotation = false;
    public bool ConstrainZRotation = false;

    [Header("Lerping")]
    public bool UseLerping = true; // Flag to enable lerping
    public float LerpSpeed = 5f;    // Lerping speed

    private Vector3 targetPosition;

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
        if (playerVelocity.x > 0)
        {
            _onPlayerLeft = true;
            _onPlayerRight = false;
        }
        else if (playerVelocity.x < 0)
        {
            _onPlayerLeft = false;
            _onPlayerRight = true;
        }
    }

    void FixedUpdate()
    {
        if(_objectToFollow == null)
        {
            _isFollowing = false;
        }

        if (_isFollowing)
        {
            SetPosition();
        }
        else if (!_isFollowing)
        {
            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _isFollowing = true;
            _objectToFollow = collision.gameObject;
            SetRotationConstraint();
            SetPosition();
            OnFollow?.Invoke(_isFollowing);
        }
    }

    private void SetPosition()
    {
        SetFollowSide();
        if (UseLerping)
        {
            // Lerp towards the target position
            targetPosition = _objectToFollow.transform.position + Offset;

            if (ConstrainXPosition)
                targetPosition.x = transform.position.x;
            if (ConstrainYPosition)
                targetPosition.y = transform.position.y;
            if (ConstrainZPosition)
                targetPosition.z = transform.position.z;

            transform.position = Vector3.Lerp(transform.position, targetPosition, LerpSpeed * Time.fixedDeltaTime);
        }
        else
        {
            // Directly set the position without lerping
            Vector3 newPosition = _objectToFollow.transform.position + Offset;

            if (ConstrainXPosition)
                newPosition.x = transform.position.x;
            if (ConstrainYPosition)
                newPosition.y = transform.position.y;
            if (ConstrainZPosition)
                newPosition.z = transform.position.z;

            transform.position = newPosition;
        }
    }

    private void SetRotationConstraint()
    {
        Quaternion targetRotation = _objectToFollow.transform.rotation;

        if (ConstrainXRotation)
        {
            targetRotation.eulerAngles = new Vector3(0f, targetRotation.eulerAngles.y, targetRotation.eulerAngles.z);
        }
        if (ConstrainYRotation)
        {
            targetRotation.eulerAngles = new Vector3(targetRotation.eulerAngles.x, 0f, targetRotation.eulerAngles.z);
        }
        if (ConstrainZRotation)
        {
            targetRotation.eulerAngles = new Vector3(targetRotation.eulerAngles.x, targetRotation.eulerAngles.y, 0f);
        }

        transform.rotation = targetRotation;
    }

    private void SetFollowSide()
    {

        if (_onPlayerLeft)
        {
            Offset = new Vector3(-1, 0.2f, 0);
        }
        else if (_onPlayerRight)
        {
            Offset = new Vector3(1, 0.2f, 0);
        }
    }
}
