using System.Collections;
using UnityEngine;
using Unity.FPS.Game;

public class ZoneSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public GameObject enemyPrefab;
    [Tooltip("Maximum simultaneous enemies this zone will hold")]
    public int maxEnemiesInZone = 1;
    [Tooltip("Delay between enemy death and respawn")]
    public float respawnDelay = 1.0f;

    [Header("Spawn Area")]
    public bool useBoxColliderAsSpawnZone = true;
    public float radius = 10f; // used if not using collider

    int currentEnemies = 0;
    BoxCollider spawnZone;

    void Start()
    {
        spawnZone = GetComponent<BoxCollider>();
        if (enemyPrefab == null)
            Debug.LogWarning($"{name}: enemyPrefab not assigned!");
        if (useBoxColliderAsSpawnZone && spawnZone == null)
            Debug.LogWarning($"{name}: No BoxCollider found! Spawning will be random in a sphere instead.");

        StartCoroutine(EnsureInitialSpawns());
    }

    IEnumerator EnsureInitialSpawns()
    {
        yield return null;
        while (currentEnemies < maxEnemiesInZone)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.1f);
        }
    }

    void SpawnEnemy()
    {
        if (!enemyPrefab)
            return;

        Vector3 spawnPos = GetRandomSpawnPosition();

        GameObject newEnemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        currentEnemies++;
        Debug.Log($"{name}: Spawned enemy at {spawnPos}");

        // Hook into Health.OnDie
        Health health = newEnemy.GetComponent<Health>();
        if (health)
        {
            UnityEngine.Events.UnityAction onDie = null;
            onDie = () =>
            {
                health.OnDie -= onDie;
                StartCoroutine(HandleEnemyDeath(newEnemy));
            };
            health.OnDie += onDie;
        }
        else
        {
            Debug.LogWarning($"{name}: Spawned enemy has no Health component!");
            StartCoroutine(WatchForDestroyedEnemy(newEnemy));
        }
    }

    Vector3 GetRandomSpawnPosition()
    {
        if (useBoxColliderAsSpawnZone && spawnZone != null)
        {
            Vector3 center = spawnZone.bounds.center;
            Vector3 size = spawnZone.bounds.size;

            float x = Random.Range(center.x - size.x / 2f, center.x + size.x / 2f);
            float y = center.y;
            float z = Random.Range(center.z - size.z / 2f, center.z + size.z / 2f);

            return new Vector3(x, y, z);
        }
        else
        {
            // fallback to spherical area
            Vector3 randomOffset = Random.insideUnitSphere * radius;
            randomOffset.y = 0; // keep on ground
            return transform.position + randomOffset;
        }
    }

    IEnumerator HandleEnemyDeath(GameObject enemy)
    {
        yield return new WaitForSeconds(respawnDelay);
        currentEnemies = Mathf.Max(0, currentEnemies - 1);
        Debug.Log($"{name}: Enemy died. Spawning new one...");
        SpawnEnemy();
    }

    IEnumerator WatchForDestroyedEnemy(GameObject enemy)
    {
        while (enemy != null)
            yield return null;

        currentEnemies = Mathf.Max(0, currentEnemies - 1);
        yield return new WaitForSeconds(respawnDelay);
        SpawnEnemy();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 1, 0, 0.25f);
        if (useBoxColliderAsSpawnZone)
        {
            BoxCollider bc = GetComponent<BoxCollider>();
            if (bc != null)
                Gizmos.DrawCube(bc.bounds.center, bc.bounds.size);
        }
        else
        {
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}