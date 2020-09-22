using UnityEngine;

public class SpreadWeapon : BaseWeapon
{
    [SerializeField] private float spread = 5f;
    [SerializeField] private int ammountOfBullets = 3;
    private Quaternion originalRot;
    protected override void Awake()
    {
        projectileBehaviour = bulletType.GetComponent<ProjectileBaseBehaviour>();
    }

    public override void Fire()
    {
        float totalAngle = (spread * ammountOfBullets) * 0.5f;
        for (int i = 0; i < ammountOfBullets; i++)
        {
            firePoint.transform.localRotation = Quaternion.Euler(totalAngle, 0, 0);
            totalAngle -= spread;
            ProjectileObjectPool.GetProjectile(projectileBehaviour.ProjectileType, firePoint.transform);
        }
        firePoint.transform.localRotation = originalRot;
    }

    public override void AssignFirePos(Transform newFirePoint)
    {
        base.AssignFirePos(newFirePoint);
        originalRot = firePoint.transform.localRotation;
    }
}
