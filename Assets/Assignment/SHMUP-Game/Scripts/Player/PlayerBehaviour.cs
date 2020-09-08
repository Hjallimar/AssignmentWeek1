using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour, IDamageableObject
{
    [SerializeField] private bool MouseMovement = false;
    [SerializeField] public List<PlayerController> controllers = new List<PlayerController>();
    
    private MovementBehaviour movementBehaviour = null;
    private PlayerWeaponBehaviour weaponBehaviour = null;

    private Vector3 direction = Vector3.zero;
    private PlayerController controller = null;

    void Awake()
    {
        movementBehaviour = GetComponent<MovementBehaviour>();
        weaponBehaviour = GetComponent<PlayerWeaponBehaviour>();

        if (MouseMovement)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;
            controller = controllers[1];
        }
        else
        {
            Cursor.visible = true;
            controller = controllers[0];
        }

        UIController.AssignPlayer(100f);
        controller.AssingVariables(movementBehaviour, transform);
    }


    void Update()
    {
        controller.GetDirectionInput();

        if (controller.GetFireInput())
        {
            weaponBehaviour.FireWeapon();
        }

        if (controller.GetStopFireInput())
        {
            weaponBehaviour.StopFire();
        }
        else if (controller.GetSwapWeaponInput())
        {
            UIController.ChangeWeapon(-1);
            Debug.Log("Swaping Weapons");
            //weaponBehaviour.SwapWeapon(1);
        }

        if (controller.GetShieldInput())
        {
            UIController.ShieldActivated(5);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            UIController.UpdatePlayerHealth(1);
            UIController.UpdatePlayerScore(20);

        }




        movementBehaviour.MoveTowards(direction);
    }

    public void TakeDamage(float damage)
    {

    }
}
