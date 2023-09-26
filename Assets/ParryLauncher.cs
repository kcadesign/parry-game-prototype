using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryLauncher : MonoBehaviour
{
    private bool _parryActive;
    private bool _forceApplied;

    [SerializeField] private float _force = 10f;

    private void OnEnable()
    {
        PlayerParry.OnParryActive += PlayerParry_OnParryActive;
    }

    private void OnDisable()
    {
        PlayerParry.OnParryActive -= PlayerParry_OnParryActive;
    }

    private void PlayerParry_OnParryActive(bool parryPressed)
    {
        _parryActive = parryPressed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Trigger entered");
        if (collision.gameObject.CompareTag("Player"))
        {
            if (_parryActive && !_forceApplied)
            {
                collision.attachedRigidbody.velocity = new(collision.attachedRigidbody.velocity.x, 0);
                collision.attachedRigidbody.AddForce(Vector2.up * _force, ForceMode2D.Impulse);
                //Debug.Log("Force applied");

                _forceApplied = true;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if (_parryActive && !_forceApplied)
            {
                collision.attachedRigidbody.velocity = new(collision.attachedRigidbody.velocity.x, 0);
                collision.attachedRigidbody.AddForce(Vector2.up * _force, ForceMode2D.Impulse);
                //Debug.Log("Force applied");

                _forceApplied = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _forceApplied = false;
    }
}
