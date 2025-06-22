using UnityEngine;

public class Collectable : MonoBehaviour
{
    private new Transform transform;
    private const float rotationSpeed = 90f;

    void Start()
    {
        transform = GetComponent<Transform>();
    }

    void Update()
    {
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + rotationSpeed * Time.deltaTime, 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameDirectionManager.Instance.ToggleDirection();
            Destroy(gameObject);
        }
    }
}
