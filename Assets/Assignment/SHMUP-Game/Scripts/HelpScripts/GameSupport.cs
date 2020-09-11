using System.ComponentModel;
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

    public static float GetLazerHitDistance(Vector3 origin)
    {
        return Mathf.Abs(origin.x - instance.bottomRight.x);
    }

    public static Vector3 ApplyCorrectPossition(Vector3 playerPos)
    {
        playerPos.x = playerPos.x < instance.topLeft.x ? instance.topLeft.x : (playerPos.x > instance.bottomRight.x ? instance.bottomRight.x : playerPos.x);
        playerPos.y = playerPos.y > instance.topLeft.y ? instance.topLeft.y : (playerPos.y < instance.bottomRight.y ? instance.bottomRight.y : playerPos.y);

        return playerPos;
    }

}

public enum TypeOfProjectile { Lazer, Missile, Spread, EnemyLazer, EnemyBullet };

public enum EnemyType { Simple = 0, Waving = 1, Charging = 2, Shooting = 3, Exploding = 4, Boss = 5 };