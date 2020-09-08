using UnityEngine;

public class SpreadWeapon : MonoBehaviour, IWeapon
{
    [SerializeField] private GameObject bulletType = null;
    [SerializeField] private float fireRate = 0.8f;
    [SerializeField] private Transform[] firePoints = new Transform[3];

    private void Awake()
    {
        PlayerWeaponBehaviour.AddWeapon(this);
    }

    public void Fire()
    {
        foreach (Transform trans in firePoints)
        {
            GameObject newBullet = Instantiate(bulletType);
            newBullet.transform.position = trans.position;
            newBullet.transform.rotation = trans.rotation;

            Destroy(newBullet, 5f);
        }
    }

    public float GetFireRate()
    {
        return fireRate;
    }
}
