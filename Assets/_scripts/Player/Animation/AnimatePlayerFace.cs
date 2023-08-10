using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimatePlayerFace : MonoBehaviour
{    
    public Rigidbody2D _playerRigidbody;

    private Animator _faceAnimator;

    private void Awake()
    {
        _faceAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        float velocityX = _playerRigidbody.velocity.x;

        _faceAnimator.SetFloat("VelocityX", velocityX);
    }
}
