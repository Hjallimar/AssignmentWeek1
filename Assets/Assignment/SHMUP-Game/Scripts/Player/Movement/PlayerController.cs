using UnityEngine;

public abstract class PlayerController : MonoBehaviour
{
    public virtual void AssingVariables(MovementBehaviour move, Transform playerTrans) { }

    public virtual void GetDirectionInput() { }

    public virtual bool GetFireInput() { return false; }

    public virtual bool GetStopFireInput() { return false; }

    public virtual int GetSwapWeaponInput() { return 1; }

    public virtual bool GetShieldInput() { return false; }
}
