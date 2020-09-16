using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class EnemyMine : EnemyBaseBehaviour
{
    [SerializeField] private Transform rotationArm;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject projectile;
    [SerializeField] private float rotationSpeed = 45f; 
    [SerializeField] private float timeBetweenFire = 0.3f;

    private float cd = 0f;
    private ProjectileBaseBehaviour behaviour;

    protected override void Awake()
    {
        behaviour = projectile.GetComponent<ProjectileBaseBehaviour>();
        base.Awake();
    }

    public override void UpdateMovement()
    {
        rotationArm.RotateAround(transform.position, Vector3.forward, rotationSpeed * Time.deltaTime);
        cd += Time.deltaTime;
        if (cd >= timeBetweenFire)
        {
            Fire();
        }
        base.UpdateMovement();
    }

    private void Fire()
    {
        if (activeStatus != true)
            return;
        cd = 0f;
        ProjectileObjectPool.GetProjectile(behaviour.ProjectileType, firePoint);
    }
}
