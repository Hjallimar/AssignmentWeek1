using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private static Spawner instance;

    [Header("Holds wave prefabs in the order they are supposed to be executed")]
    [SerializeField] private GameObject[] wavePrefabs = new GameObject[0];

    private Dictionary<GameObject, IWave> waves;

    [Header("A check weather the spawning should follow the waves or spawn randomly")]
    [SerializeField] private bool RandomizedSpawn = true;

    [Header("Spawnpoints for the randomized spawner")]
    [SerializeField] private Transform[] spawnPoints = new Transform[0];
    [Header("")]
    [SerializeField] private Transform bossSpawnPoint = null;
    [Header("Set the time between every random spawned object")]
    [SerializeField] private float timeBetweenSpawn = 2f;
    [Header("Enemies in array goes from lower Challange to higher challange")]
    [SerializeField] private EnemyType[] spawnableEnemyTypes = new EnemyType[0];
    [Header("")]
    [SerializeField] private int spawnsBeforeBossSpawn = 10;

    private bool spawnable = false;
    private int spawnCounter = 0;
    private float timeCounter = 0f;
    private int currentWave = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Update()
    {
        if (instance.RandomizedSpawn && instance.spawnable)
        {
            instance.timeCounter += Time.deltaTime;

            if (instance.timeCounter >= instance.timeBetweenSpawn)
            {
                int randSpawn = 0;
                if (instance.spawnCounter >= instance.spawnsBeforeBossSpawn)
                {
                    GameObject spawnedEnemy = EnemyObjectPool.GetEnemy(EnemyType.Boss);
                    randSpawn = Random.Range(0, spawnPoints.Count());
                    spawnedEnemy.transform.position = instance.bossSpawnPoint.position;
                    spawnedEnemy.transform.rotation = instance.bossSpawnPoint.rotation;
                    instance.spawnable = false;
                    instance.spawnCounter = 0;
                }
                else
                {
                    randSpawn = Random.Range(0, instance.spawnableEnemyTypes.Count());
                    GameObject spawnedEnemy = EnemyObjectPool.GetEnemy(spawnableEnemyTypes[randSpawn]);
                    randSpawn = Random.Range(0, instance.spawnPoints.Count());
                    spawnedEnemy.transform.position = instance.spawnPoints[randSpawn].position;
                    spawnedEnemy.transform.rotation = instance.spawnPoints[randSpawn].rotation;
                    instance.timeCounter = 0;
                    instance.spawnCounter ++;
                }
            }
        }
    }

    public static void ActivateSpawner(bool status)
    {
        Debug.Log("Welcome, you are here");
        instance.RandomizedSpawn = status;
        Debug.Log("status is: " + status + ", and randomize is: " + instance.RandomizedSpawn);
        if (!instance.RandomizedSpawn)
        {
            if (instance.wavePrefabs.Count() == 0)
            {
                Debug.LogError("There are no waves in the wavePrefab array. Settings returned to default.");
                instance.RandomizedSpawn = true;
                instance.spawnable = true;
                return;
            }
            Debug.Log("Going with waves");
            foreach (GameObject gO in instance.wavePrefabs)
            {
                IWave behaviour = gO.GetComponent<IWave>();
                if (behaviour != null)
                {
                    instance.waves.Add(gO, behaviour);
                }
            }
            UIController.UpdateWaveInfo("Starting wave " + (instance.currentWave + 1));

            instance.waves[instance.wavePrefabs[instance.currentWave]].ActivateWave();
        }
        else
        {
            Debug.Log("Going with random");
            instance.spawnable = true;
        }
    }

    public static void StartNewWave()
    {
        instance.currentWave++;
        if (instance.currentWave < instance.wavePrefabs.Count())
        {
            instance.waves[instance.wavePrefabs[instance.currentWave]].ActivateWave();
            UIController.UpdateWaveInfo("Starting wave " + (instance.currentWave + 1));
        }
        else
        {
            UIController.UpdateWaveInfo("No waves left, resuming with random spawns");
            instance.RandomizedSpawn = true;
            instance.spawnable = true;
        }
    }
}
