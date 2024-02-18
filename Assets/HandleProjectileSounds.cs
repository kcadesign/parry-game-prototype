using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleProjectileSounds : MonoBehaviour
{
    public SoundCollection _projectileSounds;

    private void OnEnable()
    {
        ProjectileController.OnProjectileShoot += ProjectileController_OnProjectileShoot;
        ProjectileController.OnProjectileDeflect += ProjectileController_OnProjectileDeflect;
    }

    private void OnDisable()
    {
        ProjectileController.OnProjectileShoot -= ProjectileController_OnProjectileShoot;
        ProjectileController.OnProjectileDeflect -= ProjectileController_OnProjectileDeflect;
    }

    private void ProjectileController_OnProjectileShoot(GameObject projectile)
    {
        if (projectile == gameObject && _projectileSounds != null && _projectileSounds.Sounds.Length > 0)
        {
            _projectileSounds.PlaySound("Shoot", transform);
        }
    }

    private void ProjectileController_OnProjectileDeflect(GameObject projectile)
    {
        if (projectile == gameObject && _projectileSounds != null && _projectileSounds.Sounds.Length > 0)
        {
            _projectileSounds.PlaySound("Deflect", transform);
        }
    }
}
