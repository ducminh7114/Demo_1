using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public EnemyWave[] enemyWaves;

    private int currentWave;

    private void Start()
    {
        currentWave = 0;
        SpawnEnemyWave();
    }

    private void SpawnEnemyWave()
    {
        if (currentWave >= enemyWaves.Length)
            return;

        EnemyWave waveInfo = enemyWaves[currentWave];

        if (waveInfo.flyPath == null ||
            waveInfo.flyPath.waypoints == null ||
            waveInfo.flyPath.waypoints.Length == 0)
        {
            Debug.LogError("FlyPath or waypoints are missing!", this);
            return;
        }

        Vector3 startPosition =
            waveInfo.flyPath.waypoints[0].transform.position;

        for (int i = 0; i < waveInfo.numberOfEnemy; i++)
        {
            Transform enemy = Instantiate(
                waveInfo.enemyPrefab,
                startPosition,
                Quaternion.identity
            );

            FlyPathAgent agent = enemy.GetComponent<FlyPathAgent>();

            if (agent != null)
            {
                agent.flyPath = waveInfo.flyPath;
                agent.flySpeed = waveInfo.speed;
            }
            else
            {
                Debug.LogError(
                    "Enemy prefab is missing FlyPathAgent!",
                    enemy
                );
            }

            startPosition += waveInfo.formationOffset;
        }

        currentWave++;

        if (currentWave < enemyWaves.Length)
        {
            Invoke(nameof(SpawnEnemyWave), waveInfo.nextWaveDelay);
        }
        else
        {
            Debug.Log("All waves have been spawned.");
        }
    }
}