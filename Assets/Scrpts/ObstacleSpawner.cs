using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs; 
    public float spawnInterval = 2f;
    public Transform spawnPointGround;
    public Transform spawnPointAir;
    public Transform leftSpawnPoint;
    public Transform rightSpawnPoint;

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnObstacle();
            timer = 0f;
        }
    }

    void SpawnObstacle()
    {
        if (!GameManager.isGameRunning) return;

        int index = Random.Range(0, obstaclePrefabs.Length);
        GameObject prefab = obstaclePrefabs[index];

        Vector3 spawnPosition = prefab.name.Contains("Air") ? spawnPointAir.position : spawnPointGround.position;

        if (GameDirectionManager.Instance.CurrentDirection == GameDirectionManager.Direction.Right)
        {
            spawnPosition.x -= 36f;
        }

        Instantiate(prefab, spawnPosition, Quaternion.identity);
    }
}
