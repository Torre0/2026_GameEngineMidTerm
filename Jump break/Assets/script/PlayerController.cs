using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Vector2 movelnput;
    private float moveSpeed = 8f;
    private float JumpForce = 9f;
    private Rigidbody2D rb;
    private Animator myAnimator;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myAnimator.SetBool("move", false);
    }
    public void OnMove(InputValue value)
    {
        movelnput = value.Get<Vector2>();
    }
    public void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, JumpForce);
        }
    }
    void Update()
    {
        if (movelnput.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (movelnput.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (movelnput.magnitude > 0)
        {
            myAnimator.SetBool("move", true);
        }
        else
        {
            myAnimator.SetBool("move", false);
        }
        transform.Translate(Vector3.right * moveSpeed * movelnput.x * Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Death")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            SceneManager.LoadScene("PlayScene_" + collision.name);
        }
    }
}
