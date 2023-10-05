using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlayerGrounded : MonoBehaviour
{
    public delegate void Grounded(bool grounded);
    public static event Grounded OnGrounded;

    
    public float raycastDistance = 0.1f;
    public int numberOfRays = 5; // Number of rays to cast
    public LayerMask groundLayer;
    
    //public LayerMask CollisionLayer;
    private bool _isGrounded = false;
    
    private void Update()
    {
        float raySpacing = GetComponent<Collider2D>().bounds.size.x / (numberOfRays - 1);

        for (int i = 0; i < numberOfRays; i++)
        {
            Vector2 rayOrigin = new Vector2(transform.position.x - GetComponent<Collider2D>().bounds.extents.x + i * raySpacing, transform.position.y);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, raycastDistance, groundLayer);

            // Visualize the rays in the Scene view
            Debug.DrawRay(rayOrigin, Vector2.down * raycastDistance, hit.collider != null ? Color.green : Color.red);

            if (hit.collider != null)
            {
                _isGrounded = true;
                //Debug.Log($"Player grounded: {_isGrounded}");
            }
            else
            {
                _isGrounded = false;
            }
        }
        OnGrounded?.Invoke(_isGrounded);
    }
    
    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((CollisionLayer.value & (1 << collision.gameObject.layer)) != 0)
        {
            _isGrounded = true;
            OnGrounded?.Invoke(_isGrounded);

        }
        //Debug.Log($"Is grounded: {_isGrounded}");

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((CollisionLayer.value & (1 << collision.gameObject.layer)) != 0)
        {
            _isGrounded = true;
            OnGrounded?.Invoke(_isGrounded);

        }
        //Debug.Log($"Is grounded: {_isGrounded}");

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _isGrounded = false;
        OnGrounded?.Invoke(_isGrounded);
        //Debug.Log($"Is grounded: {_isGrounded}");

    }
    */
}
