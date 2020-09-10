using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Player Stats")]
public class PlayerStats : ScriptableObject
{
    [field: SerializeField] public float Health { get; set; } = 10f;
    [field: SerializeField] public float MovemtnSpeed { get; set; } = 10f;
    [field: SerializeField] public float ShieldDuration { get; set; } = 3f;
    [field: SerializeField] public float ShieldCooldown { get; set; } = 15f;



}
