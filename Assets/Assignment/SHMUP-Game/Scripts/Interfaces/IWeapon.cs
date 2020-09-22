using UnityEngine;

public interface IWeapon 
{
    float GetFireRate();
    void Fire();

    Sprite GetWeaponSprite();

    void AssignFirePos(Transform firePoint);
}
