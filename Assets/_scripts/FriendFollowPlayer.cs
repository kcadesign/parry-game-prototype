using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendFollowPlayer : MonoBehaviour
{
    [Header("Position Parameters")]
    public GameObject ObjectToFollow;
    public Vector3 Offset;
    private bool _onPlayerLeft;
    private bool _onPlayerRight;

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
    private bool _friendDeployed = false;

    private void Awake()
    {
        SetRotationConstraint();
    }

    private void OnEnable()
    {
        FriendController.OnFriendDeployed += FriendController_OnFriendDeployed;
        PlayerMove.OnPlayerMoveInput += PlayerMove_OnPlayerMoveInput;
    }

    private void OnDisable()
    {
        FriendController.OnFriendDeployed -= FriendController_OnFriendDeployed;
        PlayerMove.OnPlayerMoveInput -= PlayerMove_OnPlayerMoveInput;
    }
    private void FriendController_OnFriendDeployed(bool friendDeployed)
    {
        _friendDeployed = friendDeployed;
    }

    private void PlayerMove_OnPlayerMoveInput(Vector2 moveVector)
    {
        // change this to be based on velocity
        if(moveVector.x > 0)
        {
            _onPlayerLeft = true;
            _onPlayerRight = false;

        }
        else if(moveVector.x < 0)
        {
            _onPlayerLeft = false;

            _onPlayerRight = true;
        }
    }

    void FixedUpdate()
    {
        SetPosition();
    }

    private void SetPosition()
    {
        SetFollowSide();
        if (UseLerping)
        {
            // Lerp towards the target position
            targetPosition = ObjectToFollow.transform.position + Offset;

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
            Vector3 newPosition = ObjectToFollow.transform.position + Offset;

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
        Quaternion targetRotation = ObjectToFollow.transform.rotation;

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
        if (_friendDeployed)
        {
            return;
        }
        else if (!_friendDeployed)
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
}
