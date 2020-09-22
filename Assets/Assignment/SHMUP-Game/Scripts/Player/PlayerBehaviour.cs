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
    private Vector3 startPosition = Vector3.zero;
    private PlayerController controller = null;
    private float currentHealth;
    private bool imortal = false;

    void Awake()
    {
        EventCoordinator.RegisterEventListener<ResetGameEventInfo>(ResetGame);
        movementBehaviour = GetComponent<MovementBehaviour>();
        weaponBehaviour = GetComponent<PlayerWeaponBehaviour>();
        movementBehaviour.MoveSpeed = stats.MovemtnSpeed;
        currentHealth = stats.Health;
        startPosition = transform.position;
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
            damage = (int)damage;
            if (damage < 1)
                damage = 1;
            currentHealth -= damage;
            UIController.UpdatePlayerHealth(damage);

            if (currentHealth <= 0)
            {
                ResetGameEventInfo Rgei = new ResetGameEventInfo(gameObject, "Player Has died");
                EventCoordinator.ActivateEvent(Rgei);
            }
            else
            {
                StartCoroutine(ImmunityTimer(0.5f));
            }
        }
    }

    public void MouseController(bool status)
    {
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
        weaponBehaviour.StartGame();
        controller.AssingVariables(movementBehaviour, transform);
    }

    private IEnumerator ImmunityTimer(float timer)
    {
        float timePassed = 0f;

        while (imortal)
        {
            timePassed += Time.deltaTime;

            if (timePassed % 5f > 0.19f)
                playerMeshRenderer.enabled = !playerMeshRenderer.enabled;

            if (timePassed >= timer)
            {
                imortal = false;
                playerMeshRenderer.enabled = true;
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    public void OnDestroy()
    {
        EventCoordinator.UnregisterEventListener<ResetGameEventInfo>(ResetGame);
    }

    public void ResetGame(EventInfo ei)
    {
        active = false;
        currentHealth = stats.Health;
        transform.position = startPosition;
        weaponBehaviour.ResetGame();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
