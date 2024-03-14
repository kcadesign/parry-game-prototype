using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionEffects : MonoBehaviour
{
    [SerializeField] private ParticleSystem _parryParticles;
    private ParticleSystem _parryParticlesInstance;

    private bool _parryActive;

    private void OnEnable()
    {
        PlayerParry.OnParryActive += PlayerParry_OnParryActive;
        //HandlePlayerCollisions.OnCollision += HandlePlayerCollisions_OnCollision;
    }

    private void OnDisable()
    {
        PlayerParry.OnParryActive -= PlayerParry_OnParryActive;
        //HandlePlayerCollisions.OnCollision -= HandlePlayerCollisions_OnCollision;
    }

    private void PlayerParry_OnParryActive(bool parryActive)
    {
        _parryActive = parryActive;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision with " + collision.gameObject.tag);
        if ((collision.gameObject.CompareTag("HurtBox") || collision.gameObject.CompareTag("Projectile")) && _parryActive)
        {
            Debug.Log("Parry collision with enemy");
            SpawnParryParticles();
        }

    }

    /*    private void HandlePlayerCollisions_OnCollision(GameObject collidedObject)
        {
            if (collidedObject.CompareTag("HurtBox") && _parryActive)
            {
                Debug.Log("Parry collision with enemy");
                SpawnParryParticles();
            }
        }
    */

    private void SpawnParryParticles()
    {
        _parryParticlesInstance = Instantiate(_parryParticles, transform.position, Quaternion.identity, gameObject.transform);
    }

}
