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
    }

    private void OnDisable()
    {
        PlayerParry.OnParryActive -= PlayerParry_OnParryActive;
    }

    private void PlayerParry_OnParryActive(bool parryActive)
    {
        _parryActive = parryActive;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Get the point of contact
        Vector2 collisionPoint = collision.ClosestPoint(transform.position);

        Debug.Log("Collision with " + collision.gameObject.tag);
        if ((collision.gameObject.CompareTag("HurtBox") || collision.gameObject.CompareTag("Projectile")) && _parryActive)
        {
            Debug.Log("Parry collision with enemy");
            SpawnParryParticles(collisionPoint, transform.position);
        }
    }

    private void SpawnParryParticles(Vector2 collisionPoint, Vector2 position)
    {
        Vector2 direction = collisionPoint - position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        _parryParticlesInstance = Instantiate(_parryParticles, position, Quaternion.identity);
        var shape = _parryParticlesInstance.shape;
        shape.rotation = new Vector3(0, 0, angle);
        _parryParticlesInstance.Play();
    }
}
