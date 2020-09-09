using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour, IDamageableObject
{
    [SerializeField] private bool MouseMovement = false;
    [SerializeField] public List<PlayerController> controllers = new List<PlayerController>();

    private bool active = false;

    private MovementBehaviour movementBehaviour = null;
    private PlayerWeaponBehaviour weaponBehaviour = null;

    private Vector3 direction = Vector3.zero;
    private PlayerController controller = null;

    void Awake()
    {
        movementBehaviour = GetComponent<MovementBehaviour>();
        weaponBehaviour = GetComponent<PlayerWeaponBehaviour>();
    }


    void Update()
    {
        if (!active)
        {
            return;
        }

        controller.GetDirectionInput();

        if (controller.GetFireInput())
        {
            if(weaponBehaviour != null)
            {
                weaponBehaviour.FireWeapon();
            }
        }

        if (controller.GetStopFireInput())
        {
            weaponBehaviour.StopFire();
        }
        
        if (controller.GetSwapWeaponInput() != 0)
        {
            UIController.ChangeWeapon(controller.GetSwapWeaponInput());
            Debug.Log("Swaping Weapons");
            weaponBehaviour.SwapWeapon(controller.GetSwapWeaponInput());
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

    public void MouseController(bool status)
    {
        Debug.Log("The status for mouse is " + status);
        MouseMovement = status;
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

        active = true;
        UIController.AssignPlayer(100f);
        controller.AssingVariables(movementBehaviour, transform);
    }
}
