using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerWeapon : MonoBehaviour, IWeapon
{
    [SerializeField] private GameObject bulletType = null;
    [SerializeField] private float fireRate = 0f;
    [SerializeField] private Sprite weaponSprite = null;

    public void Fire()
    {

    }
    public float GetFireRate()
    {
        return 1f;
    }

    public Sprite GetSprite()
    {
        return weaponSprite;
    }
}
