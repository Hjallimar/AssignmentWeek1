using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour, IWeapon
{

    [SerializeField] protected GameObject bulletType = null;
    [SerializeField] protected float fireRate = 0.8f;
    [SerializeField] protected Sprite weaponSprite;
    protected ProjectileBaseBehaviour projectileBehaviour;
    protected Transform firePoint = null;
    // Start is called before the first frame update
    protected virtual void Awake()
    {
        
    }

    public virtual void AssignFirePos(Transform newFirePoint)
    {
        firePoint = newFirePoint;
    }

    public virtual Sprite GetWeaponSprite()
    {
        return weaponSprite;
    }

    public virtual float GetFireRate()
    {
        return fireRate;
    }

    public virtual void Fire()
    {
    }
}
