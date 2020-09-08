using UnityEngine;

public class KeyboardController : PlayerController
{
    private MovementBehaviour movementBehaviour = null;
    Vector3 direction = Vector3.zero;

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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            return true;
        }
        return false;
    }

    public override bool GetStopFireInput()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            return true;
        }
        return false;
    }

    public override bool GetSwapWeaponInput()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            return true;
        }
        return false;
    }
}
