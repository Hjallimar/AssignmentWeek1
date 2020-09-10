using UnityEngine;

public class LazerWeapon : MonoBehaviour, IWeapon
{
    [SerializeField] private GameObject bulletType = null;
    [SerializeField] private float fireRate = 0.3f;
    [SerializeField] private Transform firePoint = null;

    private ProjectileBaseBehaviour projectileBehaviour;
    private void Awake()
    {
        PlayerWeaponBehaviour.AddWeapon(this);

        projectileBehaviour = bulletType.GetComponent<ProjectileBaseBehaviour>();
    }

    public void Fire()
    {
        GameObject newBullet = ProjectileObjectPool.GetProjectile(projectileBehaviour.ProjectileType);
        newBullet.transform.position = firePoint.position;
        newBullet.transform.rotation = firePoint.rotation;
    }

    public float GetFireRate()
    {
        return fireRate;
    }
}
