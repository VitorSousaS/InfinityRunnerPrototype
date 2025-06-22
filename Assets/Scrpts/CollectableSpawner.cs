using UnityEngine;

public class CollectableSpawner : MonoBehaviour
{
    private GameObject currentCollectable;
    public GameObject collectablePrefab;
    public Transform[] spawnPoints;
    public float spawnInterval = 4f;

    private float timer = 0f;

    void Update()
    {
        if (!GameManager.isGameRunning) return;

        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnCollectable();
            timer = 0f;
        }
    }

    void SpawnCollectable()
    {
        if (currentCollectable != null)
            Destroy(currentCollectable);

        int index = Random.Range(0, spawnPoints.Length);
        Vector3 spawnPosition = spawnPoints[index].position;

        if (GameDirectionManager.Instance.CurrentDirection == GameDirectionManager.Direction.Right)
        {
            spawnPosition.x -= 36f;
        }

        Instantiate(collectablePrefab, spawnPosition, Quaternion.identity);
    }
}
