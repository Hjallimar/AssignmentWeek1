using UnityEngine;

public class SpreadWeapon : BaseWeapon
{
    [SerializeField] private float spread = 5f;
    [SerializeField] private int ammountOfBullets = 3;
    [SerializeField] private Transform spreadFirePoint;
    protected override void Awake()
    {
        projectileBehaviour = bulletType.GetComponent<ProjectileBaseBehaviour>();
    }

    public override void Fire()
    {
        float totalAngle = (spread * ammountOfBullets) * 0.5f;
        for (int i = 0; i < ammountOfBullets; i++)
        {
            spreadFirePoint.transform.localRotation = Quaternion.Euler(totalAngle, 0, 0);
            totalAngle -= spread;
            ProjectileObjectPool.GetProjectile(projectileBehaviour.ProjectileType, spreadFirePoint.transform);
        }
    }

    public override void AssignFirePos(Transform newFirePoint)
    {
        base.AssignFirePos(newFirePoint);
        spreadFirePoint.position = newFirePoint.position;
        spreadFirePoint.rotation = newFirePoint.rotation;
    }
}
