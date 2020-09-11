using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : EnemyBaseBehaviour
{
    [SerializeField] private Transform[] firePoints = new Transform[0];
    [SerializeField] private float maxTravel = 6f;
    [SerializeField] private string bossName = "";
    [SerializeField] private GameObject primary;
    [SerializeField] private GameObject secondary;

    private ProjectileBaseBehaviour primaryFire;
    private ProjectileBaseBehaviour secondaryFire;

    [SerializeField] private float TimeBetweenAttacks = 3f;

    private bool imortal = true;
    protected bool primaryProjectile = true;
    protected Coroutine firePatrol;

    protected override void Awake()
    {
        primaryFire = primary.GetComponent<ProjectileBaseBehaviour>();
        secondaryFire = secondary.GetComponent<ProjectileBaseBehaviour>();
        base.Awake();
    }

    public override void UpdateMovement()
    {
        if (imortal)
        {
            if(transform.position.x < maxTravel)
            {
                imortal = false;
                UIController.AssignBoss(bossName, currentHealth);
                firePatrol = StartCoroutine(FireWeapons());
            }
            base.UpdateMovement();
        }
    }

    public override void TakeDamage(float dmg)
    {
        if (!imortal)
        {
            base.TakeDamage(dmg);
        }
    }

    public override void ActivateObject()
    {
        imortal = true;
        primaryProjectile = true;
        base.ActivateObject();
    }

    public override void ReturnToObjectPool()
    {
        StopFire();
        BossDefeatedEventInfo Bdei = new BossDefeatedEventInfo(gameObject, "I've have died");
        EventCoordinator.ActivateEvent(Bdei);
        EnemyObjectPool.AddEnemyToPool(gameObject);
    }

    protected void ChangeFire()
    {
        StopFire();
        primaryProjectile = !primaryProjectile;
        firePatrol = StartCoroutine(FireWeapons());
    }

    protected void StopFire()
    {
        StopCoroutine(firePatrol);
    }

    protected virtual IEnumerator FireWeapons()
    {
        float timer = 0f;
        int counter = 0;
        if (primaryProjectile)
        {
            while (primaryProjectile)
            {
                timer += Time.deltaTime;
                if (timer >= TimeBetweenAttacks)
                {
                    foreach (Transform trans in firePoints)
                    {
                        GameObject projectile = ProjectileObjectPool.GetProjectile(primaryFire.ProjectileType, trans);
                    }
                    timer = 0;
                    counter++;
                }
                if(counter >= 3)
                {
                    ChangeFire();
                }
                yield return new WaitForSeconds(Time.deltaTime);
            }
        }
        else
        {
            while (true)
            {
                float laserTimer = 0;
                foreach (Transform trans in firePoints)
                {
                    while (laserTimer < 0.5f)
                    {
                        laserTimer += Time.deltaTime;
                        yield return new WaitForSeconds(Time.deltaTime);
                    }
                    GameObject projectile = ProjectileObjectPool.GetProjectile(secondaryFire.ProjectileType, trans);
                }
                ChangeFire();
            }

        }
    }
}
