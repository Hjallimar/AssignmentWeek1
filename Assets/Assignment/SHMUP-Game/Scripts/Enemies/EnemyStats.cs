using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : ScriptableObject
{
    [field: SerializeField] public float Health { get; set; } = 10f;
    [field: SerializeField] public float Speed { get; set; } = 10f;
}
