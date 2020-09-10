using UnityEngine;

public class GameSupport : MonoBehaviour
{
    private static GameSupport instance;

    private RaycastHit hit;

    [Header("Use this to limit the players movement on the screen")]
    [SerializeField] private Vector2 topLeft = Vector3.zero;
    [SerializeField] private Vector2 bottomRight = Vector3.zero;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public static Vector3 ApplyCorrectPossition(Vector3 playerPos)
    {
        playerPos.x = playerPos.x < instance.topLeft.x ? instance.topLeft.x : (playerPos.x > instance.bottomRight.x ? instance.bottomRight.x : playerPos.x);
        playerPos.y = playerPos.y > instance.topLeft.y ? instance.topLeft.y : (playerPos.y < instance.bottomRight.y ? instance.bottomRight.y : playerPos.y);

        return playerPos;
    }

}

public enum TypeOfProjectile { Lazer, Missile, Spread };

public enum EnemyType { Simple, Waving, Charging, Shooting, Exploding };