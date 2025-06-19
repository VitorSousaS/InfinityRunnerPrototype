using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        if (!GameManager.isGameRunning) return;

        Vector2 newPosition = rb.position + Vector2.left * speed * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);

        if (newPosition.x < -15f)
        {
            Destroy(gameObject);
        }
    }
}
