using UnityEngine;

public class KeyboardController : PlayerController
{
    private MovementBehaviour movementBehaviour = null;
    Vector3 direction = Vector3.zero;

    [SerializeField] private KeyCode fireButton = KeyCode.Space;
    [SerializeField] private KeyCode shieldButton = KeyCode.Tab;
    [SerializeField] private KeyCode swapWeaponButtonRight = KeyCode.E;
    [SerializeField] private KeyCode swapWeaponButtonLeft = KeyCode.Q;

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

    public override int GetSwapWeaponInput()
    {
        if (Input.GetKeyDown(swapWeaponButtonRight))
        {
            return 1;
        }
        else if (Input.GetKeyDown(swapWeaponButtonLeft))
        {
            return -1;
        }
        return 0;
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
