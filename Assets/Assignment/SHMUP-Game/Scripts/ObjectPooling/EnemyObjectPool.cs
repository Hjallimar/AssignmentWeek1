﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObjectPool : MonoBehaviour
{
    private static EnemyObjectPool instance;

    [SerializeField] private List<GameObject> enemiesPrefabs = new List<GameObject>();
    private Dictionary<EnemyType, List<GameObject>> enemyPool = new Dictionary<EnemyType, List<GameObject>>();
    private List<GameObject> removeFromActive = new List<GameObject>();
    private Dictionary<GameObject, EnemyBaseBehaviour> activeEnemies = new Dictionary<GameObject, EnemyBaseBehaviour>();

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
            activeEnemies.Remove(go);
        }
        removeFromActive.Clear();

        if (activeEnemies.Count > 0)
        {
            try
            {
                foreach (KeyValuePair<GameObject, EnemyBaseBehaviour> entry in activeEnemies)
                {
                    entry.Value.UpdateMovement();
                }
            }
            catch (InvalidOperationException e)
            {
                Debug.Log("You have recived a " + e.ToString());
            }
        }
    }

    public static void AddMeToActive(GameObject newEnemy)
    {
        if (instance != null)
        {
            Debug.Log("Adding Enemy to active list: " + newEnemy.name);
            EnemyBaseBehaviour enemyBehaviour = newEnemy.GetComponent<EnemyBaseBehaviour>();
            if (enemyBehaviour != null)
            {
                if (instance.activeEnemies.ContainsKey(newEnemy))
                {
                    Debug.Log("Enemy is already active");
                }
                else
                {
                    instance.activeEnemies.Add(newEnemy, enemyBehaviour);
                }
            }
        }
    }

    public static void AddEnemyToPool(GameObject enemy)
    {
        EnemyBaseBehaviour behaviour = enemy.GetComponent<EnemyBaseBehaviour>();
        enemy.SetActive(false);
        if (instance.activeEnemies.ContainsKey(enemy))
        {
            instance.removeFromActive.Add(enemy);
        }

        if (instance.enemyPool.ContainsKey(behaviour.Type))
        {
            instance.enemyPool[behaviour.Type].Add(enemy);
        }
        else
        {
            List<GameObject> newList = new List<GameObject>();
            newList.Add(enemy);
            instance.enemyPool.Add(behaviour.Type, newList);
        }
        behaviour.SetActive(false);
    }

    public static GameObject GetEnemy(EnemyType type)
    {
        if (instance.enemyPool.ContainsKey(type))
        {
            if (instance.enemyPool[type].Count > 0)
            {
                GameObject enemy = instance.enemyPool[type][0];
                enemy.SetActive(true);
                instance.enemyPool[type].RemoveAt(0);
                instance.activeEnemies.Add(enemy, enemy.GetComponent<EnemyBaseBehaviour>());
                return enemy;
            }
            return instance.CreateNewEnemy(type);

        }
        else
        {
            return instance.CreateNewEnemy(type);
        }
    }

    public static Transform GetActiveEnemy()
    {
        if (instance.activeEnemies.Count > 0)
        {
            foreach(KeyValuePair<GameObject, EnemyBaseBehaviour> entry in instance.activeEnemies)
            {
                return entry.Key.transform;
            }
        }
        return null;
    }

    private GameObject CreateNewEnemy(EnemyType type)
    {
        foreach (GameObject enemy in instance.enemiesPrefabs)
        {
            EnemyBaseBehaviour enemyBehaviour = enemy.GetComponent<EnemyBaseBehaviour>();
            if (enemyBehaviour.Type == type)
            {
                GameObject newEnemy = Instantiate(enemy);
                instance.activeEnemies.Add(newEnemy, newEnemy.GetComponent<EnemyBaseBehaviour>());
                return newEnemy;
            }
        }
        Debug.Log("No enemy of that type exists");
        return null;
    }

    private void AddPoolBase()
    {
        foreach (GameObject enemy in instance.enemiesPrefabs)
        {
            for (int i = 0; i < 5; i++)
            {
                EnemyBaseBehaviour prefabBehaviour = enemy.GetComponent<EnemyBaseBehaviour>();
                GameObject newEnemy = Instantiate(enemy);
                AddEnemyToPool(newEnemy);
            }
        }
    }

    public void OnDestroy()
    {
        EventCoordinator.UnregisterEventListener<ResetGameEventInfo>(ResetGame);
    }

    private static void ResetGame(EventInfo ei)
    {
        foreach(KeyValuePair<GameObject, EnemyBaseBehaviour> entry in instance.activeEnemies)
        {
            AddEnemyToPool(entry.Key);
            
        }
        
        instance.removeFromActive.Clear();
        instance.activeEnemies.Clear();
    }
}
