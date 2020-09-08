using UnityEngine;

public class LazerWeapon : MonoBehaviour, IWeapon
{
    [SerializeField] private GameObject bulletType = null;
    [SerializeField] private float fireRate = 0.3f;
    [SerializeField] private Transform firePoint = null;

    private void Awake()
    {
        PlayerWeaponBehaviour.AddWeapon(this);
    }

    public void Fire()
    {
        GameObject newBullet = Instantiate(bulletType);
        newBullet.transform.position = firePoint.position;
        newBullet.transform.rotation = firePoint.rotation;
    }

    public float GetFireRate()
    {
        return fireRate;
    }
}
