using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWave : MonoBehaviour, IWave
{
    [SerializeField] private List<WaveComponent> waveComponents = new List<WaveComponent>();
    [SerializeField] private float waitUntilNextWave = 5f;
    private List<GameObject> enemiesInWave = new List<GameObject>();

    private void Start()
    {
        EventCoordinator.RegisterEventListener<BossDefeatedEventInfo>(BossDefeated);
        EventCoordinator.RegisterEventListener<DefeatedEnemyEventInfo>(DefeatedEnemy);
    }

    public void ActivateWave()
    {
        foreach(WaveComponent component in waveComponents)
        {
            GameObject enemy = EnemyObjectPool.GetEnemy(component.GetEnemyType());
            Debug.Log("An enemy should be added here");
            enemy.transform.position = component.transform.position;
            enemy.transform.rotation = component.transform.rotation;
            enemiesInWave.Add(enemy);
        }
    }

    //This is going to be callback
    public void DefeatedEnemy(EventInfo ei)
    {
        if (enemiesInWave.Contains(ei.GO))
        {
            enemiesInWave.Remove(ei.GO);
            if (enemiesInWave.Count == 0)
            {
                Spawner.StartNewWave(waitUntilNextWave);
            }
        }
    }
    
    public void BossDefeated(EventInfo ei)
    {
        if (enemiesInWave.Contains(ei.GO))
        {
            enemiesInWave.Remove(ei.GO);
        }
    }
    
    void OnDestroy()
    {
        EventCoordinator.UnregisterEventListener<BossDefeatedEventInfo>(BossDefeated);
        EventCoordinator.UnregisterEventListener<DefeatedEnemyEventInfo>(DefeatedEnemy);
    }
}
