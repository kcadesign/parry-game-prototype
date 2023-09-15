using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimatePlayerMovement : MonoBehaviour
{    
    public Rigidbody2D _playerRigidbody;
    private Animator _faceAnimator;

    private void Awake()
    {
        _faceAnimator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        float velocityX = _playerRigidbody.velocity.x;

        _faceAnimator.SetFloat("VelocityX", velocityX);

    }

}
