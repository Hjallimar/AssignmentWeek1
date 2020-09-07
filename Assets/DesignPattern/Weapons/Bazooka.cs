using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bazooka : MonoBehaviour, I_Weapon
{
    // Start is called before the first frame update
    public void Fire()
    {
        Debug.Log("I go boom");
    }
}
