using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckEnemyGrounded : MonoBehaviour
{
    public float raycastDistance = 0.6f;
    public LayerMask groundLayer;

    private static bool _isGrounded;
    public static bool IsGrounded
    {
        get { return _isGrounded; }
    }

    private void Update()
    {
        CheckIfGrounded();
    }

    public void CheckIfGrounded()
    {
        // Cast a ray downward from the player's position in 2D
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, raycastDistance, groundLayer);

        if (hit.collider != null)
        {
            // Draw the raycast in the Scene view
            Debug.DrawRay(transform.position, Vector2.down * raycastDistance, Color.green);

            _isGrounded = true;
        }
        else
        {
            // Draw the raycast in the Scene view
            Debug.DrawRay(transform.position, Vector2.down * raycastDistance, Color.red);

            _isGrounded = false;
        }
    }
}
