using System.Collections.Generic;
using UnityEngine;

public class EnemyWave : MonoBehaviour, IWave
{
    [SerializeField] private List<WaveComponent> waveComponents = new List<WaveComponent>();
    private List<GameObject> enemiesInWave = new List<GameObject>();

    private void Start()
    {
        EventCoordinator.RegisterEventListener<DefeatedEnemyEventInfo>(DefeatedEnemy);
    }

    public void ActivateWave()
    {
        foreach(WaveComponent component in waveComponents)
        {
            GameObject enemy = EnemyObjectPool.GetEnemy(component.GetEnemyType());
            enemy.transform.position = component.transform.position;
            enemy.transform.rotation = component.transform.rotation;
            enemiesInWave.Add(enemy);
        }
    }

    //This is going to be callback
    public void DefeatedEnemy(EventInfo ei)
    {
        DefeatedEnemyEventInfo deei = (DefeatedEnemyEventInfo)ei;

        if (enemiesInWave.Contains(deei.GO))
        {
            enemiesInWave.Remove(deei.GO);
            if (enemiesInWave.Count == 0)
            {
                Spawner.StartNewWave();
            }
        }
    }

    void OnDestroy()
    {
        EventCoordinator.UnregisterEventListener < DefeatedEnemyEventInfo>(DefeatedEnemy);
    }
}
