using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour, I_Weapon
{
    public void Fire()
    {
        Debug.Log("I go pew");
    }
}
