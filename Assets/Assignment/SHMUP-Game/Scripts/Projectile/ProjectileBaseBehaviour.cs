using UnityEngine;

public class ProjectileBaseBehaviour : MonoBehaviour
{
    [SerializeField] protected ProjectileStats stats;
    [SerializeField] protected LayerMask hitLayers;

    [field: SerializeField] public TypeOfProjectile ProjectileType { get; set; } = default; 

    protected RaycastHit hit;
    protected float distance = 0f;

    public virtual void UpdateProjectileMovement()
    {
        distance = stats.MoveSpeed * Time.deltaTime;
        CalculateColission();
    }

    protected virtual void TargetHit()
    {
        IDamageableObject target = hit.collider.GetComponent<IDamageableObject>();
        if (target != null)
        {
            target.TakeDamage(stats.Damage);
        }
    }

    protected void CalculateColission()
    {
        if(Physics.Raycast(transform.position, transform.forward, out hit, distance, hitLayers))
        {
            TargetHit();
            ReturnToPool();
        }

        transform.position += transform.forward * distance;
    }

    public void ReturnToPool()
    {
        ProjectileObjectPool.AddProjectileToPool(gameObject);
    }


}
