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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // --- Horizontal movement (A/D or Left/Right) ---
        float move = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(move * moveSpeed, rb.linearVelocityY);

        // --- Check if grounded (tiny raycast under feet) ---
        isGrounded = Physics2D.Raycast(groundContact.position, Vector2.down, .15f, groundLayer);

        // --- Jump (W, Up Arrow) ---
        if (isGrounded && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}
