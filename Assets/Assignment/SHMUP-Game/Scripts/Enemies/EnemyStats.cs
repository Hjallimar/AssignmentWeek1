using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/EnemyStats")]
public class EnemyStats : ScriptableObject
{
    private static float nonActiveSpeed = 7f;
    [field: SerializeField] public float NonActiveSpeed { get { return nonActiveSpeed; } }
    [field: SerializeField] public float Health { get; set; } = 10f;
    [field: SerializeField] public float ActiveSpeed { get; set; } = 10f;
    [field: SerializeField] public float Score { get; set; } = 10f;
    [field: SerializeField] public float Damage { get; set; } = 10f;
}
