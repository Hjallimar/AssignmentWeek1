using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Projectile Stats")]
public class ProjectileStats : ScriptableObject
{
    [field: SerializeField] public float MoveSpeed { get; set; } = 10f;
    [field: SerializeField] public float Damage { get; set; } = 10f;
}
