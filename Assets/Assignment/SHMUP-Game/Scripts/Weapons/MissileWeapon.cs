using UnityEngine;

public class MissileWeapon : BaseWeapon
{
    protected override void Awake()
    {
        projectileBehaviour = bulletType.GetComponent<ProjectileBaseBehaviour>();
    }

    public override void Fire()
    {
        GameObject newBullet = ProjectileObjectPool.GetProjectile(projectileBehaviour.ProjectileType, firePoint);
    }
}
