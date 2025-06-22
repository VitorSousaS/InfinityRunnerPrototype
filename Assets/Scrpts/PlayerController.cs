using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }
    public float jumpForce = 7f;
    private Rigidbody2D rb;
    private bool isGrounded = true;
    private PlayerInputActions inputActions;
    private bool isDead = false;
    private Vector3 initialPosition;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        inputActions = new PlayerInputActions();
        inputActions.Gameplay.Jump.performed += OnJump;
        rb = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        OnDisable();
    }

    void OnEnable()
    {
        inputActions.Gameplay.Enable();
    }

    void OnDisable()
    {
        inputActions.Gameplay.Disable();
    }

    void Update()
{
    float angleMultiplier = 1f;

    if (GameDirectionManager.Instance.CurrentDirection == GameDirectionManager.Direction.Right)
    {
        angleMultiplier = -1f;
    }

    float targetAngle = Mathf.Clamp(rb.linearVelocity.y * 5f, -45f, 45f) * angleMultiplier;
    Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);
    transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
}


    private void OnJump(InputAction.CallbackContext context)
    {
        if (isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if (collision.collider.CompareTag("Obstacle") && !isDead)
        {
            isDead = true;
            rb.linearVelocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Static;
            GameManager.Instance.GameOver();
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Collectable"))
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
    }
    public void ResetPlayer()
    {
        isDead = false;
        isGrounded = true;
        transform.position = initialPosition;
        rb.bodyType = RigidbodyType2D.Dynamic;
        spriteRenderer.flipX = true;
    }

    public void StartPlayer()
    {
        OnEnable();        
    }
}