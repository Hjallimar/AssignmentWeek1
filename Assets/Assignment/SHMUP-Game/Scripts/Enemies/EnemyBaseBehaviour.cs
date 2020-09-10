using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public abstract class EnemyBaseBehaviour : MonoBehaviour, IDamageableObject
{
    [SerializeField] protected EnemyStats stats;
    [SerializeField] protected LayerMask collisionLayers;
    [field: SerializeField] public EnemyType Type { get; set; } = default;
    protected float currentHealth;
    protected float currentSpeed;
    protected SphereCollider hitCollider = null;

    protected float distance = 0f;
    protected RaycastHit hit;

    protected virtual void Awake()
    {
        hitCollider = GetComponent<SphereCollider>();
        ActivateObject();
    }

    public virtual void UpdateMovement() 
    {
        distance = hitCollider.radius + (stats.Speed * Time.deltaTime);
        CheckForCollision(distance);
    }

    public virtual void TakeDamage(float dmg)
    {
        currentHealth -= dmg;
        if (currentHealth <= 0)
        {
            ReturnToObjectPool();
        }
    }

    public virtual void CheckForCollision(float distance)
    {
        if (Physics.SphereCast(transform.position, hitCollider.radius, transform.forward, out hit, distance, collisionLayers))
        {
            IDamageableObject target = hit.collider.GetComponent<IDamageableObject>();
            if (target != null)
            {
                target.TakeDamage(currentHealth/3);
            }
        }
        else
        {
            transform.position += transform.forward * distance;
        }
    }

    protected void ReturnToObjectPool()
    {
        EnemyObjectPool.AddEnemyToPool(gameObject);
    }

    public void ActivateObject()
    {
        currentHealth = stats.Health;
        currentSpeed = stats.Speed;
    }
}

