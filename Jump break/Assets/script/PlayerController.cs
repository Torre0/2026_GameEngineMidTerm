using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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

    bool isInvincible = false;

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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Respawn"))
        {
            if (!isInvincible)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        if (collision.CompareTag("Finish"))
        {
            collision.GetComponent<LevelObject>().MoveToNextLevel();
        }
        if (collision.CompareTag("Enemy"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
            if (collision.CompareTag("Item_Invincible"))
        {
            Destroy(collision.gameObject);
            StartCoroutine(InvincibilityRoutine(5f));
            return;
        }

        if (collision.CompareTag("Item_Speed"))
        {
            Destroy(collision.gameObject);
            StartCoroutine(SpeedRoutine(5f, 3f));
            return;
        }

        if (collision.CompareTag("Item_Jump"))
        {
            Destroy(collision.gameObject);
            StartCoroutine(JumpRoutine(5f, 3f));
            return;
        }
    }
    IEnumerator InvincibilityRoutine(float duration)
    {
        isInvincible = true;
        Debug.Log("무적 시작!");

        Color originalColor = sr.color;
        sr.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0.5f);

        yield return new WaitForSeconds(duration);

        sr.color = originalColor;
        isInvincible = false;
        Debug.Log("무적 종료!");
    }
    IEnumerator SpeedRoutine(float duration, float boost)
    {
        float original = moveSpeed;
        moveSpeed = original + boost;

        yield return new WaitForSeconds(duration);

        moveSpeed = original;
    }
    IEnumerator JumpRoutine(float duration, float boost)
    {
        float original = jumpForce;
        jumpForce = original + boost;

        yield return new WaitForSeconds(duration);

        jumpForce = original;
    }
}
