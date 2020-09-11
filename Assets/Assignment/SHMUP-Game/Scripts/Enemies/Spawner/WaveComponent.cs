using UnityEngine;

public class WaveComponent : MonoBehaviour
{
    [SerializeField] private EnemyType enemyType;

    public EnemyType GetEnemyType()
    {
        return enemyType;
    }
}
