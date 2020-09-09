using UnityEngine;

public class MouseController : PlayerController
{
    Transform playerTrans = default;
    private MovementBehaviour movementBehaviour = null;
    Vector3 direction = Vector3.zero;

    public override void AssingVariables(MovementBehaviour move, Transform player)
    {
        playerTrans = player;
        movementBehaviour = move;
    }

    public override void GetDirectionInput()
    {
        if (movementBehaviour == null || playerTrans == null)
        {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        direction = ray.origin - playerTrans.position;
        direction.z = 0;
        movementBehaviour.MoveTowards(direction);
    }

    public override bool GetFireInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            return true;
        }
        return false;
    }

    public override bool GetStopFireInput()
    {
        if (Input.GetMouseButtonUp(0))
        {
            return true;
        }
        return false;
    }

    public override int GetSwapWeaponInput()
    {
        if (Input.mouseScrollDelta.y > 0)
        {
            return 1;
        }
        else if (Input.mouseScrollDelta.y < 0)
        {
            return -1;
        }
        return 0;
    }

    public override bool GetShieldInput()
    {
        if (Input.GetMouseButtonDown(1))
        {
            return true;
        }
        return false;
    }
}
