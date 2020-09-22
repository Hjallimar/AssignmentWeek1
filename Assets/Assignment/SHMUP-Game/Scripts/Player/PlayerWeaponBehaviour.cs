using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponBehaviour : MonoBehaviour
{
    private static List<IWeapon> weaponList = new List<IWeapon>();

    [SerializeField] private Transform firePointOrigin;
    private static Transform firePoint;
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
        firePoint = firePointOrigin;
    }

    public void Update()
    {
        if(currentWeapon == null && weaponList.Count > 0)
        {
            currentWeapon = weaponList[0];
        }

        coolDown += coolDown > 3 ? 0 : Time.deltaTime;
    }

    public static void AddWeapon(IWeapon newWeapon)
    {
        if (!weaponList.Contains(newWeapon))
        {
            newWeapon.AssignFirePos(firePoint);
            weaponList.Add(newWeapon);
            UIController.AddNewWeaponSprite(newWeapon.GetWeaponSprite());
        }
    }

    public void SwapWeapon(int i)
    {
        weaponIndex += i;
        if (weaponIndex > weaponList.Count - 1)
        {
            weaponIndex = 0;
        }
        else if (weaponIndex < 0)
        {
            weaponIndex = weaponList.Count - 1;
        }
        currentWeapon = weaponList[weaponIndex];
        weaponFireRate = currentWeapon.GetFireRate();
    }

    public void FireWeapon()
    {
        if (currentWeapon != null)
        {
            fireLoop = StartCoroutine(FiringWeapon());
        }
    }

    public void StopFire()
    {
        if (fireLoop != null )
        {
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
                currentWeapon.Fire();
                coolDown = 0;
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}
