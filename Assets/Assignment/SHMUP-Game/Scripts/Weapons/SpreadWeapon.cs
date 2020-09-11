using UnityEngine;

public class SpreadWeapon : MonoBehaviour, IWeapon
{
    [SerializeField] private GameObject bulletType = null;
    [SerializeField] private float fireRate = 0.8f;
    [SerializeField] private Transform[] firePoints = new Transform[3];

    private ProjectileBaseBehaviour projectileBehaviour;

    private void Awake()
    {
        PlayerWeaponBehaviour.AddWeapon(this);
        projectileBehaviour = bulletType.GetComponent<ProjectileBaseBehaviour>();
    }

    public void Fire()
    {
        foreach (Transform trans in firePoints)
        {
            GameObject newBullet = ProjectileObjectPool.GetProjectile(projectileBehaviour.ProjectileType, trans);
        }
    }

    public float GetFireRate()
    {
        return fireRate;
    }
}
