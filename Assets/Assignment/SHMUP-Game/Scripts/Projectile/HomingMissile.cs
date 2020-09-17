using UnityEngine;

public class HomingMissile : ProjectileBaseBehaviour
{
    [SerializeField] protected float accelerate = 5f;
    [SerializeField] protected ParticleSystem particles = default;
    protected Transform target;

    protected override void Awake()
    {
        EventCoordinator.RegisterEventListener<UpdateProjectileMovementEventInfo>(UpdateProjectileMovement);
        EventCoordinator.RegisterEventListener<DefeatedEnemyEventInfo>(OnEnemyDefeated);
    }

    public override void ReActivate()
    {
        particles.Play();
        target = EnemyObjectPool.GetActiveEnemy();
    }

    public override void UpdateProjectileMovement(EventInfo ei)
    {
        if(ei.GO == gameObject)
        {
            if (target != null)
            {
                transform.LookAt(target);
            }
            currentSpeed += accelerate * Time.deltaTime;
            CalculateMovement();
        }
    }

    public override void ReturnToPool()
    {
        particles.Stop();
        base.ReturnToPool();
    }

    public void OnEnemyDefeated(EventInfo ei)
    {
        if (ei.GO.transform == target)
        {
            Debug.Log(gameObject.name + " got it, removing " + ei.GO.name + " as target");
            target = null;
        }
    }

    protected override void OnDestroy()
    {
        EventCoordinator.UnregisterEventListener<UpdateProjectileMovementEventInfo>(UpdateProjectileMovement);
        EventCoordinator.UnregisterEventListener<DefeatedEnemyEventInfo>(OnEnemyDefeated);
    }

}
