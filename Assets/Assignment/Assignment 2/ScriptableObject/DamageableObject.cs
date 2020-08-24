using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableObject : MonoBehaviour
{
    [SerializeField] private WallBase myStats = null;

    // Start is called before the first frame update
    void Start()
    {
        myStats.AssingMyGameObject(gameObject);
    }

    public void OnDamageTaken(float damage)
    {
        myStats.TakeDamage(damage);
    }

}
