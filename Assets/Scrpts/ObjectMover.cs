using UnityEngine;

public class ObjectMover : MonoBehaviour
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

        Vector2 moveDir = GameDirectionManager.Instance.GetDirectionVector();
        Vector2 newPosition = rb.position + speed * Time.fixedDeltaTime * moveDir;

        rb.MovePosition(newPosition);

        if (Mathf.Abs(newPosition.x) > 19f)
        {
            Destroy(gameObject);
        }
    }

}
