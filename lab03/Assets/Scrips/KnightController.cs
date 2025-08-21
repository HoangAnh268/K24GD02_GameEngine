using UnityEngine;

public class KnightController : MonoBehaviour
{
    public Animator animator;
    private Rigidbody2D rb;
    public float moveSpeed = 3f;
    public float jumpForce = 5f;
    public float bounceForce = 7f;

    private bool isJumping = false;
    private bool isGrounded = false;
    private SpriteRenderer spriteRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        var moveInput = Input.GetAxis("Horizontal");
        var move_x = moveInput * moveSpeed;
        rb.linearVelocity = new Vector2(move_x, rb.linearVelocity.y);
        animator.SetBool("isRunning", moveInput != 0);
       // animator.SetBool("isIdle", moveInput == 0);
        if (moveInput > 0)
            spriteRenderer.flipX = false;
        else if (moveInput < 0)
            spriteRenderer.flipX = true;

        if (Input.GetKeyDown(KeyCode.Space) &&  isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            animator.SetBool("isJump", true);
            //isJumping = true;
            isGrounded = false;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("OnCollisionEnter2D");
        if (collision.gameObject.CompareTag("Ground"))
        {
            //isJumping = false;
            isGrounded = true;
            animator.SetBool("isJump", false);
            //Debug.Log("Đã chạm đất");
        }      
    }
    
    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.SetBool("isJump", false);
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            animator.SetBool("isJump", true);
        }
    }
    public void KillEnemy(GameObject enemy)
    {
        Destroy(enemy); // Enemy biến mất
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, bounceForce); // Player sau khi nhảy lên enemy sẽ bị bật lên
    }

    // Hàm gọi khi bị Enemy chạm ngang
    public void Die()
    {
        //Destroy(gameObject); // Player biến mất
        Debug.Log("Game Over!");
        ScoreManager.instance.GameOver();
        gameObject.SetActive(false); // Ẩn player thay vi xóa player
    }
}
