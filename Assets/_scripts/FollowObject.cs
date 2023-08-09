using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    [Header("Position Parameters")]
    public GameObject ObjectToFollow;
    [SerializeField] private Vector3 _offset;

    [Header("Rotation Constraints")]
    public bool ConstrainXRotation = false;
    public bool ConstrainYRotation = false;
    public bool ConstrainZRotation = false;

    void Update()
    {
        SetPosition();
        SetRotationConstraint();
    }

    private void SetPosition()
    {
        gameObject.transform.position = ObjectToFollow.transform.position + _offset;
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

        gameObject.transform.rotation = targetRotation;
    }
}