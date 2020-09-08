using UnityEngine;

public abstract class PlayerController : MonoBehaviour
{
    public virtual void AssingVariables(MovementBehaviour move, Transform playerTrans) { }

    public virtual void GetDirectionInput() { }

    public virtual bool GetFireInput() { return false; }

    public virtual bool GetStopFireInput() { return false; }

    public virtual bool GetSwapWeaponInput() { return false; }
}
