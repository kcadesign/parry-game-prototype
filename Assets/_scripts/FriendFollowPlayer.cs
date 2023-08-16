using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendFollowPlayer : MonoBehaviour
{
    [Header("Position Parameters")]
    public GameObject ObjectToFollow;
    [SerializeField] private Vector3 _offset;

    [Header("Rotation Constraints")]
    public bool ConstrainXRotation = false;
    public bool ConstrainYRotation = false;
    public bool ConstrainZRotation = false;

    [Header("Lerping")]
    public bool UseLerping = true; // Flag to enable lerping
    public float LerpSpeed = 5f;    // Lerping speed

    private Vector3 targetPosition;

    void FixedUpdate()
    {
        SetPosition();
        SetRotationConstraint();
    }

    private void SetPosition()
    {
        if (UseLerping)
        {
            // Lerp towards the target position
            targetPosition = ObjectToFollow.transform.position + _offset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, LerpSpeed * Time.fixedDeltaTime);
        }
        else
        {
            // Directly set the position without lerping
            transform.position = ObjectToFollow.transform.position + _offset;
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
}
