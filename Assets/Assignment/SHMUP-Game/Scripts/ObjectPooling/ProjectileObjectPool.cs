using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ProjectileObjectPool : MonoBehaviour
{
    private static ProjectileObjectPool instance;


    [SerializeField] private List<GameObject> projectilePrefabs = new List<GameObject>();
    private Dictionary<TypeOfProjectile, List<GameObject>> projectilePool = new Dictionary<TypeOfProjectile, List<GameObject>>();

    private Dictionary<GameObject, ProjectileBaseBehaviour> activeProjectiles = new Dictionary<GameObject, ProjectileBaseBehaviour>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            AddPoolBase();
        }
    }

    private void Update()
    {
        foreach (KeyValuePair<GameObject, ProjectileBaseBehaviour> entry in activeProjectiles)
        {
            entry.Value.UpdateProjectileMovement();
        }
    }

    public static void AddProjectileToPool(GameObject projectile)
    {
        ProjectileBaseBehaviour behaviour = projectile.GetComponent<ProjectileBaseBehaviour>();
        projectile.SetActive(false);
        if (instance.activeProjectiles.ContainsKey(projectile))
        {
            instance.activeProjectiles.Remove(projectile);
        }

        if (instance.projectilePool.ContainsKey(behaviour.ProjectileType))
        {
            Debug.Log("Projectile type Existed so this was added.");
            instance.projectilePool[behaviour.ProjectileType].Add(projectile);
        }
        else
        {
            Debug.Log("No projectile of this type existed so a new objectPool was added");
            List<GameObject> newList = new List<GameObject>();
            newList.Add(projectile);
            instance.projectilePool.Add(behaviour.ProjectileType, newList);
        }
    }

    public static GameObject GetProjectile(TypeOfProjectile type)
    {
        if (instance.projectilePool.ContainsKey(type))
        {
            if(instance.projectilePool[type].Count > 0)
            {
                GameObject projectile = instance.projectilePool[type][0];
                projectile.SetActive(true);
                instance.projectilePool[type].RemoveAt(0);
                instance.activeProjectiles.Add(projectile, projectile.GetComponent<ProjectileBaseBehaviour>());
                return projectile;
            }

            return instance.CreateNewProjectile(type);

        }
        else
        {
            return instance.CreateNewProjectile(type);
        }
    }

    private GameObject CreateNewProjectile(TypeOfProjectile type)
    {
        foreach (GameObject projectile in instance.projectilePrefabs)
        {
            ProjectileBaseBehaviour prefabBehaviour = projectile.GetComponent<ProjectileBaseBehaviour>();
            if (prefabBehaviour.ProjectileType == type)
            {
                GameObject newProjectile = Instantiate(projectile);
                instance.activeProjectiles.Add(newProjectile, newProjectile.GetComponent<ProjectileBaseBehaviour>());
                return newProjectile;
            }
        }
        Debug.Log("No projectile of that type exists");
        return null;
    }

    private void AddPoolBase()
    {
        foreach (GameObject projectile in instance.projectilePrefabs)
        {
            for (int i = 0; i < 5; i++)
            {
                ProjectileBaseBehaviour prefabBehaviour = projectile.GetComponent<ProjectileBaseBehaviour>();
                GameObject newProjectile = Instantiate(projectile);
                AddProjectileToPool(newProjectile);
            }
        }
    }
}
