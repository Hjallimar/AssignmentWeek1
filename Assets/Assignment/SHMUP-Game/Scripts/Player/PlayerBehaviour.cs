using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour, IDamageableObject
{
    [SerializeField] private List<PlayerController> controllers = new List<PlayerController>();
    [SerializeField] private PlayerStats stats = null;
    [SerializeField] private Renderer playerMeshRenderer = null;

    private bool MouseMovement = false;
    private bool active = false;

    private MovementBehaviour movementBehaviour = null;
    private PlayerWeaponBehaviour weaponBehaviour = null;

    private Vector3 direction = Vector3.zero;
    private PlayerController controller = null;
    private float currentHealth;
    private bool imortal = false;

    void Awake()
    {
        movementBehaviour = GetComponent<MovementBehaviour>();
        weaponBehaviour = GetComponent<PlayerWeaponBehaviour>();
        movementBehaviour.MoveSpeed = stats.MovemtnSpeed;
        currentHealth = stats.Health;
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
            imortal = true;
            StartCoroutine(ImmunityTimer(stats.ShieldDuration));
            UIController.ShieldActivated(stats.ShieldCooldown);
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
        if (!imortal)
        {
            currentHealth -= damage;
            UIController.UpdatePlayerHealth(damage);
            StartCoroutine(ImmunityTimer(0.5f));
        }
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
        UIController.AssignPlayer(currentHealth);
        controller.AssingVariables(movementBehaviour, transform);
    }

    private IEnumerator ImmunityTimer(float timer)
    {
        float timePassed = 0f;
        while (imortal)
        {
            timePassed += Time.deltaTime;

            playerMeshRenderer.enabled = !playerMeshRenderer.enabled;

            if (timePassed >= timer)
            {
                imortal = false;
                playerMeshRenderer.enabled = true;
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}
