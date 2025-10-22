using UnityEngine;

public class ZoneSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;   // assign your enemy prefab in Inspector
    public BoxCollider spawnZone;    // assign the BoxCollider here

    public float spawnInterval = 2f; // seconds between spawns

    void Start()
    {
        if (spawnZone == null)
            spawnZone = GetComponent<BoxCollider>(); // automatically finds BoxCollider on same GameObject
        InvokeRepeating(nameof(SpawnEnemy), 0f, spawnInterval);
    }

    void SpawnEnemy()
    {
        Vector3 pos = GetRandomPointInBox(spawnZone);
        Instantiate(enemyPrefab, pos, Quaternion.identity);
    }

    Vector3 GetRandomPointInBox(BoxCollider box)
    {
        Vector3 size = box.size;
        Vector3 localPos = new Vector3(
            Random.Range(-size.x / 2f, size.x / 2f),
            Random.Range(-size.y / 2f, size.y / 2f),
            Random.Range(-size.z / 2f, size.z / 2f)
        );
        return box.transform.TransformPoint(box.center + localPos);
    }

    void OnDrawGizmosSelected()
    {
        if (spawnZone != null)
        {
            Gizmos.color = Color.green;
            Gizmos.matrix = spawnZone.transform.localToWorldMatrix;
            Gizmos.DrawWireCube(spawnZone.center, spawnZone.size);
        }
    }
}