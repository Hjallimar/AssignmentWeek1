using UnityEngine;

public class ProjectileBaseBehaviour : MonoBehaviour
{
    [SerializeField] protected ProjectileStats stats;
    [SerializeField] protected LayerMask hitLayers;

    [field: SerializeField] public TypeOfProjectile ProjectileType { get; set; } = default; 

    protected RaycastHit hit;
    protected float distance = 0f;
    protected float currentSpeed;

    protected virtual void Awake()
    {
        EventCoordinator.RegisterEventListener<UpdateProjectileMovementEventInfo>(UpdateProjectileMovement);
    }

    protected virtual void OnEnable()
    {
        currentSpeed = stats.MoveSpeed;
    }

    public virtual void UpdateProjectileMovement(EventInfo ei)
    {
        UpdateProjectileMovementEventInfo Upmei = (UpdateProjectileMovementEventInfo)ei;
        if (ei.GO == gameObject)
        {
            CalculateMovement();
        }
    }

    public virtual void CalculateMovement()
    {
        distance = currentSpeed * Time.deltaTime;
        CalculateColission();
    }

    public virtual void ReActivate()
    {

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

    public virtual void ReturnToPool()
    {
        ProjectileObjectPool.AddProjectileToPool(gameObject);
    }

    protected virtual void OnDestroy()
    {
        EventCoordinator.UnregisterEventListener<UpdateProjectileMovementEventInfo>(UpdateProjectileMovement);
    }

}
