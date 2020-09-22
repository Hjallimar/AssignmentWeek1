using System.Collections;
using UnityEngine;

public class ExplodingEnemy : EnemyBaseBehaviour
{
    [SerializeField] private float explosionRadius = 5f;
    [SerializeField] private LayerMask explosionLayers = default;
    [SerializeField] private ParticleSystem explosion = null;
    [SerializeField] private Renderer bombRenderer = null;
    bool killed = false;
    Collider[] hits = new Collider[0];
    public override void TakeDamage(float dmg)
    {
        currentHealth -= dmg;
        if (currentHealth < 0)
        {
            if (killed == false)
            {
                killed = true;
                StartCoroutine(KilledByPlayer());
            }
        }
    }

    protected virtual IEnumerator KilledByPlayer()
    {
        hits = Physics.OverlapSphere(transform.position, explosionRadius, explosionLayers, QueryTriggerInteraction.Ignore);

        foreach (Collider coll in hits)
        {
            IDamageableObject dmgObj = coll.GetComponent<IDamageableObject>();
            if (dmgObj != null)
            {
                dmgObj.TakeDamage(stats.Damage);
            }
        }
        explosion.Play();
        bombRenderer.enabled = false;

        while (explosion.isPlaying)
        {
            yield return new WaitForSeconds(Time.deltaTime);
        }

        ReturnToObjectPool();
        bombRenderer.enabled = true;
        killed = false;
    }
}
