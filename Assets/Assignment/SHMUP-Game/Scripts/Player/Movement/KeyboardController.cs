using UnityEngine;

public class KeyboardController : PlayerController
{
    private MovementBehaviour movementBehaviour = null;
    Vector3 direction = Vector3.zero;

    [SerializeField] private KeyCode fireButton;
    [SerializeField] private KeyCode shieldButton;
    [SerializeField] private KeyCode swapWeaponButton;

    public override void AssingVariables(MovementBehaviour move, Transform player)
    {
        movementBehaviour = move;
    }

    public override void GetDirectionInput()
    {
        if (movementBehaviour == null)
        {
            return;
        }
        direction.x = Input.GetAxis("Horizontal");
        direction.y = Input.GetAxis("Vertical");
        movementBehaviour.MovePlayer(direction);
    }

    public override bool GetFireInput()
    {
        if (Input.GetKeyDown(fireButton))
        {
            return true;
        }
        return false;
    }

    public override bool GetStopFireInput()
    {
        if (Input.GetKeyUp(fireButton))
        {
            return true;
        }
        return false;
    }

    public override bool GetSwapWeaponInput()
    {
        if (Input.GetKeyDown(swapWeaponButton))
        {
            return true;
        }
        return false;
    }

    public override bool GetShieldInput()
    {
        if (Input.GetKeyDown(shieldButton))
        {
            return true;
        }
        return false;
    }
}
