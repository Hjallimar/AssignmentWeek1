using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public abstract class EnemyBaseBehaviour : MonoBehaviour, IDamageableObject, IEnemy
{
    [SerializeField] protected EnemyStats stats;
    [SerializeField] protected LayerMask collisionLayers;
    [SerializeField] protected bool addInStart = false;

    [field: SerializeField] public EnemyType Type { get; set; } = default;
    protected float currentHealth;
    protected float currentSpeed;
    protected SphereCollider hitCollider = null;

    protected float distance = 0f;
    protected RaycastHit hit;
    protected bool activeStatus = false; 

    protected virtual void Awake()
    {
        hitCollider = GetComponent<SphereCollider>();
        ActivateObject();
        if (addInStart)
        {
            EnemyObjectPool.AddMeToActive(gameObject);
        }
        currentSpeed = stats.NonActiveSpeed;
    }

    public virtual void UpdateMovement() 
    {
        distance = (currentSpeed * Time.deltaTime);
        CheckForCollision(distance);
    }

    public virtual void TakeDamage(float dmg)
    {
        currentHealth -= dmg;
        if (currentHealth <= 0)
        {
            UIController.UpdatePlayerScore(stats.Score);
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
                ReturnToObjectPool();
            }
        }
        else
        {
            transform.position += transform.forward * distance;
        }
    }
    public virtual void ReturnToObjectPool()
    {
        DefeatedEnemyEventInfo Deei = new DefeatedEnemyEventInfo(gameObject, "I've have died");
        EventCoordinator.ActivateEvent(Deei);
        EnemyObjectPool.AddEnemyToPool(gameObject);
    }

    public virtual void ActivateObject()
    {
        currentHealth = stats.Health;
        currentSpeed = stats.NonActiveSpeed;
    }

    public virtual void SetActive(bool status)
    {
        float oldspeed = currentSpeed;
        if (status)
        {
            currentSpeed = stats.ActiveSpeed;
        }
        else
        {
            currentSpeed = stats.NonActiveSpeed;
        }
        activeStatus = status;
    }
}

