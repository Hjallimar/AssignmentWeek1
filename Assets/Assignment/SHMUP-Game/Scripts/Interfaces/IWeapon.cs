using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon 
{
    float GetFireRate();
    void Fire();
    Sprite GetSprite();
}
