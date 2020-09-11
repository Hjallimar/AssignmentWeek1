using System;
using System.Collections;
using UnityEngine;

public class LazerBullet : ProjectileBaseBehaviour
{
    [SerializeField] private ParticleSystem particles;
    [SerializeField] private float delayBeforeFire = 0f;
    protected RaycastHit[] hit = new RaycastHit[0];
    private float rayDistance;

    public override void UpdateProjectileMovement(EventInfo ei)
    {
    }

    public override void ReActivate()
    {
        StartCoroutine(ShowRay());
    }

    protected virtual void FireLazer()
    {
        rayDistance = GameSupport.GetLazerHitDistance(transform.position);
        hit = Physics.RaycastAll(transform.position, transform.forward, rayDistance, hitLayers);
        if(hit.Length != 0)
        {
            foreach (RaycastHit target in hit)
            {
                IDamageableObject targetHit = target.collider.GetComponent<IDamageableObject>();
                if (targetHit != null)
                {
                    targetHit.TakeDamage(stats.Damage);
                }
            }
        }
    }

    protected virtual IEnumerator ShowRay()
    {
        float timer = 0;
        while (timer < delayBeforeFire)
        {
            timer += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        FireLazer();
        particles.Play();
        timer = 0;
        while (timer < 0.1f)
        {
            timer += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        particles.Stop();
        ReturnToPool();
    }
}
