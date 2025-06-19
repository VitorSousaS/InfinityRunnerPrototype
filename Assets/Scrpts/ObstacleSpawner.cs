using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public float spawnInterval = 2f;
    public Transform spawnPoint;

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
        if (GameManager.isGameRunning)
        {
            Instantiate(obstaclePrefab, spawnPoint.position, Quaternion.identity);
        }
    }

    public void RemoveAllObstacles()
    {
        ObstacleMover[] obstacles = FindObjectsByType<ObstacleMover>(FindObjectsSortMode.None);
        foreach (ObstacleMover obstacle in obstacles)
        {
            Destroy(obstacle.gameObject);
        }
    }
}