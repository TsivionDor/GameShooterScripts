using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{

    [System.Serializable]
    private class EnemySpawnInfo
    {
        public GameObject enemyPrefab;
        public float delayBeforeSpawning;
        public float timeBetweenSpawns;
        public int count;
        public int initialHealth; 
    }

    [System.Serializable]
    private class EnemyWave
    {
        public List<EnemySpawnInfo> spawnInfos;
    }

    [Header("Spawn Configuration")]
    [SerializeField] private float radius = 5f;
    [SerializeField] private float timer = 0f;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float maxDistanceFromPlayer = 30f;
    [SerializeField] private List<EnemyWave> waves;

    [Header("Player Configuration")]
    [SerializeField] private Transform playerTransform;

    private int waveNumber = 0;
    private List<GameObject> activeEnemies = new List<GameObject>();
    private Vector3 lastPlayerPosition;

    private void Start()
    {
        StartCoroutine(SpawnWaves());
        lastPlayerPosition = playerTransform.position;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        TeleportDistantEnemies();
    }

    private void OnDrawGizmos()
    {
        if (playerTransform != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(playerTransform.position, radius);
        }
    }

    // --------- Custom Methods -------------

    private void TeleportDistantEnemies()
    {
        Vector3 currentPlayerDirection = (playerTransform.position - lastPlayerPosition).normalized;
        Vector3 perpDirection = new Vector3(-currentPlayerDirection.y, currentPlayerDirection.x, currentPlayerDirection.z);

        foreach (GameObject enemy in activeEnemies)
        {
            if (enemy == null) continue;

            float distanceToPlayer = Vector3.Distance(playerTransform.position, enemy.transform.position);
            if (distanceToPlayer > maxDistanceFromPlayer)
            {
                Vector3 spawnBasePosition = playerTransform.position + currentPlayerDirection * (radius + 2);
                float randomOffset = Random.Range(-radius, radius);
                Vector3 newSpawnPosition = spawnBasePosition + perpDirection * randomOffset;

                enemy.transform.position = newSpawnPosition;
            }
        }

        lastPlayerPosition = playerTransform.position;
    }

    private IEnumerator SpawnWaves()
    {
        while (waveNumber < waves.Count)
        {
            timer = 0f;
            EnemyWave currentWave = waves[waveNumber];

            List<Coroutine> spawnRoutines = new List<Coroutine>();
            foreach (var spawnInfo in currentWave.spawnInfos)
            {
                spawnRoutines.Add(StartCoroutine(SpawnEnemyRoutine(spawnInfo)));
            }

            foreach (var routine in spawnRoutines)
            {
                yield return routine;
            }

            while (activeEnemies.Exists(enemy => enemy != null))
            {
                yield return new WaitForSeconds(0.5f);
            }

            waveNumber++;
            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }
    public void SetEnemyHealthInCurrentWave(int enemyIndex, int newHealth)
    {
        if (waveNumber < waves.Count && enemyIndex < activeEnemies.Count && activeEnemies[enemyIndex] != null)
        {
            EnemyBase enemy = activeEnemies[enemyIndex].GetComponent<EnemyBase>();
            if (enemy != null)
            {
                enemy.SetHealth(newHealth);
            }
        }
    }
    private IEnumerator SpawnEnemyRoutine(EnemySpawnInfo spawnInfo)
    {
        yield return new WaitForSeconds(spawnInfo.delayBeforeSpawning);

        for (int enemyCount = 0; enemyCount < spawnInfo.count; enemyCount++)
        {
            SpawnEnemy(spawnInfo.enemyPrefab, spawnInfo.initialHealth); // Passing initial health here
            yield return new WaitForSeconds(spawnInfo.timeBetweenSpawns);
        }
    }

    private void SpawnEnemy(GameObject enemyPrefab, int health = -1)
    {
        Vector3 randomPositionInCircle = playerTransform.position + (Quaternion.Euler(0, 0, Random.Range(0, 360)) * Vector3.up * radius);
        GameObject enemy = Instantiate(enemyPrefab, randomPositionInCircle, spawnPoint.rotation);
        if (health > 0)
        {
            EnemyBase enemyBase = enemy.GetComponent<EnemyBase>();
            if (enemyBase != null)
            {
                enemyBase.SetHealth(health);
            }
        }
        activeEnemies.Add(enemy);
    }
}
