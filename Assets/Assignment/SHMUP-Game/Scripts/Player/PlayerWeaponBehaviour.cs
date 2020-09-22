using System.Collections;
using UnityEngine;

public class PlayerWeaponBehaviour : MonoBehaviour
{
    private static IWeapon[] weaponList = new IWeapon[3];

    [SerializeField] private Transform firePointOrigin = null;
    [SerializeField] private BaseWeapon startWeapon = null;
    private static Transform firePoint;
    private IWeapon currentWeapon = null;

    private Coroutine fireLoop = default;

    private int currentWeaponIndex = 0;
    private float weaponFireRate = 0;
    float coolDown = 0;
    private static int weaponIndex = -1;

    public void Awake()
    {
        if (weaponList.Length != 0)
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
        if(currentWeapon == null && weaponList.Length > 0)
        {
            currentWeapon = weaponList[0];
        }

        coolDown += coolDown > 3 ? 0 : Time.deltaTime;
    }

    public void StartGame()
    {
        AddWeapon(startWeapon);
    }

    public static void AddWeapon(IWeapon newWeapon)
    {
        foreach(IWeapon weapon in weaponList)
        {
            if (weapon == newWeapon)
            {
                return;
            }
        }

        weaponIndex++;
        weaponList[weaponIndex] = newWeapon;
        newWeapon.AssignFirePos(firePoint);
        UIController.AddNewWeaponSprite(newWeapon.GetWeaponSprite());

    }

    public void SwapWeapon(int i)
    {
        currentWeaponIndex += i;
        if (currentWeaponIndex > weaponIndex)
        {
            currentWeaponIndex = 0;
        }
        else if (currentWeaponIndex < 0)
        {
            currentWeaponIndex = weaponIndex;
        }
        currentWeapon = weaponList[currentWeaponIndex];
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

    public void ResetGame()
    {
        weaponIndex = -1;
        currentWeapon = null;
        StopCoroutine(fireLoop);
        weaponList = new IWeapon[3];
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
