using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGrounded : MonoBehaviour
{
    public delegate void Grounded(bool grounded);
    public static event Grounded OnGrounded;

    public float raycastDistance = 0.1f;
    public LayerMask groundLayer;  // Layer mask for ground objects

    private bool _isGrounded = false;

    private void Update()
    {
        // Cast a ray downward from the player's position in 2D
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, raycastDistance, groundLayer);

        if (hit.collider != null)
        {
            // Player is on the ground
            Debug.Log("Player is on the ground");

            // Draw the raycast in the Scene view
            Debug.DrawRay(transform.position, Vector2.down * raycastDistance, Color.green);

            _isGrounded = true;
            OnGrounded?.Invoke(_isGrounded);
        }
        else
        {
            // Player is in the air
            Debug.Log("Player is in the air");

            // Draw the raycast in the Scene view
            Debug.DrawRay(transform.position, Vector2.down * raycastDistance, Color.red);

            _isGrounded = false;
            OnGrounded?.Invoke(_isGrounded);

        }
    }
}
