using System;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileObjectPool : MonoBehaviour
{
    private static ProjectileObjectPool instance;

    [SerializeField] private List<GameObject> projectilePrefabs = new List<GameObject>();
    private Dictionary<TypeOfProjectile, List<GameObject>> projectilePool = new Dictionary<TypeOfProjectile, List<GameObject>>();
    private List<GameObject> removeFromActive = new List<GameObject>();
    private Dictionary<GameObject, ProjectileBaseBehaviour> activeProjectiles = new Dictionary<GameObject, ProjectileBaseBehaviour>();

    private void Awake()
    {
        EventCoordinator.RegisterEventListener<ResetGameEventInfo>(ResetGame);

        if (instance == null)
        {
            instance = this;

            AddPoolBase();
        }
    }

    private void LateUpdate()
    {
        foreach (GameObject go in removeFromActive)
        {
            instance.activeProjectiles.Remove(go);
        }
        removeFromActive.Clear();
        if (activeProjectiles.Count > 0)
        {
            try
            {
                foreach (KeyValuePair<GameObject, ProjectileBaseBehaviour> entry in activeProjectiles)
                {
                    UpdateProjectileMovementEventInfo Upmei = new UpdateProjectileMovementEventInfo(entry.Key, "Updating movement");
                    EventCoordinator.ActivateEvent(Upmei);
                }
            }
            catch (InvalidOperationException e)
            {
                Debug.Log("You have recived a " + e.ToString());
            }
        }
    }

    public static void AddProjectileToPool(GameObject projectile)
    {
        ProjectileBaseBehaviour behaviour = projectile.GetComponent<ProjectileBaseBehaviour>();
        projectile.SetActive(false);
        if (instance.activeProjectiles.ContainsKey(projectile))
        {
            instance.removeFromActive.Add(projectile);
        }

        if (instance.projectilePool.ContainsKey(behaviour.ProjectileType))
        {
            instance.projectilePool[behaviour.ProjectileType].Add(projectile);
        }
        else
        {
            List<GameObject> newList = new List<GameObject>();
            newList.Add(projectile);
            instance.projectilePool.Add(behaviour.ProjectileType, newList);
        }
    }

    public static GameObject GetProjectile(TypeOfProjectile type, Transform location)
    {
        if (instance.projectilePool.ContainsKey(type))
        {
            if(instance.projectilePool[type].Count > 0)
            {
                GameObject projectile = instance.projectilePool[type][0];
                if (instance.activeProjectiles.ContainsKey(projectile))
                {
                    Debug.Log("Oh Shit");
                }
                ProjectileBaseBehaviour behaviour = projectile.GetComponent<ProjectileBaseBehaviour>();
                projectile.SetActive(true);
                instance.projectilePool[type].RemoveAt(0);
                instance.activeProjectiles.Add(projectile, behaviour);
                projectile.transform.position = location.position;
                projectile.transform.rotation = location.rotation;
                behaviour.ReActivate();
                return projectile;
            }

            return instance.CreateNewProjectile(type, location);

        }
        else
        {
            return instance.CreateNewProjectile(type, location);
        }
    }

    private GameObject CreateNewProjectile(TypeOfProjectile type, Transform location)
    {
        foreach (GameObject projectile in instance.projectilePrefabs)
        {
            ProjectileBaseBehaviour prefabBehaviour = projectile.GetComponent<ProjectileBaseBehaviour>();
            if (prefabBehaviour.ProjectileType == type)
            {
                GameObject newProjectile = Instantiate(projectile);
                ProjectileBaseBehaviour behaviour = newProjectile.GetComponent<ProjectileBaseBehaviour>();
                instance.activeProjectiles.Add(newProjectile, behaviour);
                newProjectile.transform.position = location.position;
                newProjectile.transform.rotation = location.rotation;
                behaviour.ReActivate();
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

    public void OnDestroy()
    {
        EventCoordinator.UnregisterEventListener<ResetGameEventInfo>(ResetGame);
    }

    private static void ResetGame(EventInfo ei)
    {
        foreach (KeyValuePair<GameObject, ProjectileBaseBehaviour> entry in instance.activeProjectiles)
        {
            AddProjectileToPool(entry.Key);
        }

        instance.activeProjectiles.Clear();
        instance.removeFromActive.Clear();
    }
}
