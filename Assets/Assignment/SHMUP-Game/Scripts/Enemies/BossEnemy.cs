using System.Collections;
using UnityEngine;

public class BossEnemy : EnemyBaseBehaviour
{
    [SerializeField] private Transform firePoint = null;
    [SerializeField] private float maxTravel = 6f;
    [SerializeField] private string bossName = "";
    [SerializeField] private GameObject fireProjectile = null;

    [SerializeField] private float ammountOfBullets = 8f;
    [SerializeField] private float spreadFireAngle = 5f;

    private ProjectileBaseBehaviour primaryFire = null;

    [SerializeField] private float TimeBetweenAttacks = 3f;

    private bool imortal = true;
    protected Coroutine firePatrol;
    protected int directionMultiplier = 1;

    protected override void Awake()
    {
        primaryFire = fireProjectile.GetComponent<ProjectileBaseBehaviour>();
        base.Awake();
    }

    public override void SetActive(bool status)
    {
        if (status)
        {
            imortal = !status;
            UIController.AssignBoss(bossName, currentHealth);
            firePatrol = StartCoroutine(FireWeapons());
        }
        base.SetActive(status);
    }

    public override void UpdateMovement()
    {
        if(transform.position.x < maxTravel)
        {
            transform.position += Vector3.up * directionMultiplier * (currentSpeed * 0.3f) * Time.deltaTime; 
            if (GameSupport.CheckMyYAxis(transform))
            {
                directionMultiplier *= -1;
            }
        }
        else
        {
            base.UpdateMovement();
        }
    }

    public override void TakeDamage(float dmg)
    {
        if (!imortal)
        {
            UIController.UpdateBossHealth(dmg);
            base.TakeDamage(dmg);
        }
    }

    public override void ActivateObject()
    {
        imortal = true;
        base.ActivateObject();
    }

    public override void ReturnToObjectPool()
    {
        StopFire();
        GameObject powerUp = Spawner.GivePowerUp();
        if (powerUp != null)
        {
            powerUp.SetActive(true);
            powerUp.transform.position = transform.position;
        }
        BossDefeatedEventInfo Bdei = new BossDefeatedEventInfo(gameObject, "I've have died");
        EventCoordinator.ActivateEvent(Bdei);
        EnemyObjectPool.AddEnemyToPool(gameObject);
    }

    protected void StopFire()
    {
        StopCoroutine(firePatrol);
    }

    protected virtual IEnumerator FireWeapons()
    {
        float timer = 0f;
        int counter = 0;
        while (true)
        {
            timer += Time.deltaTime;
            if (timer >= TimeBetweenAttacks)
            {
                float totalAngle = (spreadFireAngle * ammountOfBullets) * 0.5f;
                for (int i = 0; i < ammountOfBullets; i++)
                {
                    firePoint.localRotation = Quaternion.Euler(totalAngle, 0, 0);
                    totalAngle -= spreadFireAngle;
                    ProjectileObjectPool.GetProjectile(primaryFire.ProjectileType, firePoint);
                }
                timer = 0;
                counter++;
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}
