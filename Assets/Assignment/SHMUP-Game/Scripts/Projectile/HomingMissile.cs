using UnityEngine;

public class HomingMissile : ProjectileBaseBehaviour
{
    [SerializeField] protected float accelerate = 5f;
    [SerializeField] protected ParticleSystem particles = default;
    protected Transform target;

    public override void ReActivate()
    {
        particles.Play();
        target = EnemyObjectPool.GetActiveEnemy();
    }

    public override void UpdateProjectileMovement()
    {
        if (target != null)
        {
            if (!target.gameObject.activeSelf)
            {
                target = null;
            }
            else
            {
                transform.LookAt(target);
            }
        }
        currentSpeed += accelerate * Time.deltaTime;
        base.UpdateProjectileMovement();
    }

    public override void ReturnToPool()
    {
        particles.Stop();
        base.ReturnToPool();
    }
}
