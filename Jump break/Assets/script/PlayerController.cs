using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public int maxJumpCount = 2;

    private Rigidbody2D rb;
    private bool isGrounded;
    private float moveInput;
    private SpriteRenderer sr;
    private Animator anim;
    private int jumpCount = 0;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        if (isGrounded && rb.linearVelocity.y <= 0)
        {
            jumpCount = 0;
        }

        Debug.Log("jumpCount: " + jumpCount);

        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        if (moveInput < 0)
            sr.flipX = true;
        else if (moveInput > 0)
            sr.flipX = false;

        anim.SetFloat("Speed", Mathf.Abs(moveInput));
        anim.SetBool("isJumping", !isGrounded);
        anim.SetFloat("yVelocity", rb.linearVelocity.y);
    }
    public void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        moveInput = input.x;
    }
    public void OnJump(InputValue value)
    {
        if (!value.isPressed) return;

        if (jumpCount < maxJumpCount)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            jumpCount++;

            if (jumpCount == 1)
            {
                anim.SetTrigger("JumpTrigger");
            }
            else if (jumpCount == 2)
            {
                anim.SetTrigger("DoubleJump");
            }
        }
    }
}
