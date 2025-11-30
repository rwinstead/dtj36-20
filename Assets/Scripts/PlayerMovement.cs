using NUnit.Framework;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] LayerMask groundLayer;
    private Rigidbody2D rb;
    [SerializeField] private bool isGrounded;
    [SerializeField] Transform groundContact;

    [SerializeField] GameObject staticSprite;
    [SerializeField] GameObject runningSprite;
    public static PlayerMovement Instance;
    BoxCollider2D playerCollider;
    bool isDead = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if(isDead) return;
        // --- Horizontal movement (A/D or Left/Right) ---
        float move = Input.GetAxisRaw("Horizontal");

        rb.linearVelocity = new Vector2(move * moveSpeed, rb.linearVelocityY);

        // --- Check if grounded (tiny raycast under feet) ---
        isGrounded = Physics2D.Raycast(groundContact.position, Vector2.down, .3f, groundLayer);

        if(move != 0 || !isGrounded)
        {
            runningSprite.SetActive(true);
            staticSprite.SetActive(false);
            playerCollider.offset = new Vector2(.3f, -.5f);
            playerCollider.size = new Vector2(5.9f, 4.1f);
        }
        else
        {
            runningSprite.SetActive(false);
            staticSprite.SetActive(true);
            playerCollider.offset = new Vector2(0f, -0.16f);
            playerCollider.size = new Vector2(4.5f, 5.6f);
        }

        // --- Jump (W, Up Arrow) ---
        if (isGrounded && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    public void FlipSpriteOnDeath()
    {
        isDead = true;
        Vector3 scale = transform.localScale;
        scale.y *= -1;
        transform.localScale = scale;
    }
}
