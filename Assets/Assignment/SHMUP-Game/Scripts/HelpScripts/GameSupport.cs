using UnityEngine;

public class GameSupport : MonoBehaviour
{
    private static GameSupport instance;

    public static GameSupport Instance { get { return instance; } }

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

    public bool CheckSphereCast(SphereCollider collider, Vector3 direction, float distance, LayerMask layermask)
    {
        if (Physics.SphereCast(collider.transform.position, collider.radius, direction, out hit, distance, layermask))
        {
            return true;
        }
        return false;
    }

    public void ApplyCorrectPossition(Transform playerTrans)
    {
        Vector3 newPos = playerTrans.position;

        newPos.x = newPos.x < instance.topLeft.x ? instance.topLeft.x : (newPos.x > instance.bottomRight.x ? instance.bottomRight.x : newPos.x);
        newPos.y = newPos.y > instance.topLeft.y ? instance.topLeft.y : (newPos.y < instance.bottomRight.y ? instance.bottomRight.y : newPos.y);

        playerTrans.position = newPos;
    }

}
