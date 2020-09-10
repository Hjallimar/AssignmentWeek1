using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/EnemyStats")]
public class EnemyStats : ScriptableObject
{
    [field: SerializeField] public float Health { get; set; } = 10f;
    [field: SerializeField] public float Speed { get; set; } = 10f;
    [field: SerializeField] public float Score { get; set; } = 10f;
}
