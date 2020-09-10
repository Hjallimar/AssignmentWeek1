using System.Collections.Generic;
using UnityEngine;

public class EnemyObjectPool : MonoBehaviour
{
    private static EnemyObjectPool instance;

    [SerializeField] private List<GameObject> enemiesPrefabs = new List<GameObject>();
    private Dictionary<EnemyType, List<GameObject>> enemyPool = new Dictionary<EnemyType, List<GameObject>>();

    private Dictionary<GameObject, EnemyBaseBehaviour> activeEnemies = new Dictionary<GameObject, EnemyBaseBehaviour>();

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
        foreach (KeyValuePair<GameObject, EnemyBaseBehaviour> entry in activeEnemies)
        {
            entry.Value.UpdateMovement();
        }
    }

    public static void AddEnemyToPool(GameObject enemy)
    {
        EnemyBaseBehaviour behaviour = enemy.GetComponent<EnemyBaseBehaviour>();
        enemy.SetActive(false);
        if (instance.activeEnemies.ContainsKey(enemy))
        {
            instance.activeEnemies.Remove(enemy);
        }

        if (instance.enemyPool.ContainsKey(behaviour.Type))
        {
            Debug.Log("Projectile type Existed so this was added.");
            instance.enemyPool[behaviour.Type].Add(enemy);
        }
        else
        {
            Debug.Log("No projectile of this type existed so a new objectPool was added");
            List<GameObject> newList = new List<GameObject>();
            newList.Add(enemy);
            instance.enemyPool.Add(behaviour.Type, newList);
        }
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
}
