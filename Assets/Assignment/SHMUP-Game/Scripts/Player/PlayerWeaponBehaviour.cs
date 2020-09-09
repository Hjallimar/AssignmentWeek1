using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponBehaviour : MonoBehaviour
{
    private static List<IWeapon> weaponList = new List<IWeapon>();

    private IWeapon currentWeapon = null;

    private Coroutine fireLoop = default;

    private int weaponIndex = 0;
    private float weaponFireRate = 0;
    float coolDown = 0;


    public void Awake()
    {
        if (weaponList.Count != 0)
        {
            currentWeapon = weaponList[0];
        }
        else
        {
            currentWeapon = null;
        }
    }

    public void Update()
    {
        coolDown += coolDown > 3 ? 0 : Time.deltaTime;
    }

    public static void AddWeapon(IWeapon newWeapon)
    {
        if (!weaponList.Contains(newWeapon))
        {
            Debug.Log("New Weapon added " + newWeapon.ToString());
            weaponList.Add(newWeapon);
        }
        else
            Debug.Log("Weapon already exists");
    }

    public void SwapWeapon(int i)
    {
        weaponIndex += i;
        Debug.Log("WeaponIndex is " + weaponIndex + ", the list holds " + weaponList.Count + " items" );
        if (weaponIndex > weaponList.Count - 1)
        {
            weaponIndex = 0;
        }
        else if (weaponIndex < 0)
        {
            weaponIndex = weaponList.Count - 1;
        }
        Debug.Log("Swapping weapon to the type " + weaponIndex);
        currentWeapon = weaponList[weaponIndex];
        weaponFireRate = currentWeapon.GetFireRate();
    }

    public void FireWeapon()
    {
        if (currentWeapon != null)
        {
            Debug.Log("Starting Fire()");
            fireLoop = StartCoroutine(FiringWeapon());
        }
    }

    public void StopFire()
    {
        if (fireLoop != null )
        {
            Debug.Log("Stopping Fire()");
            StopCoroutine(fireLoop);
        }
    }

    private IEnumerator FiringWeapon()
    {
        weaponFireRate = currentWeapon.GetFireRate();
        while (true)
        {
            if (coolDown >= weaponFireRate)
            {
                Debug.Log("Firing a bullet");
                currentWeapon.Fire();
                coolDown = 0;
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
