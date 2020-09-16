using UnityEngine;

public class WaveComponent : MonoBehaviour
{
    [SerializeField] private EnemyType enemyType = default;

    public EnemyType GetEnemyType()
    {
        return enemyType;
    }
}
