using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    //Variables
    [SerializeField] float moveSpeed = 40f;
    [SerializeField] float jumpVelocity = 10f;
    public bool isRunning = false;
    float inputHorizontal;
    bool facingRight = true;

    //References
    Rigidbody2D rigidbody2d;
    CapsuleCollider2D capsuleCollider2D;
    BoxCollider2D boxCollider2D;
    [SerializeField] LayerMask platformsLayerMask;
    Animator animator;

    private void Awake()
    {
        rigidbody2d = transform.GetComponent<Rigidbody2D>();
        boxCollider2D = transform.GetComponent<BoxCollider2D>();
        capsuleCollider2D = transform.GetComponent<CapsuleCollider2D>();
        animator = transform.GetComponent<Animator>();
    }

    private void Update()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");

        Movement();

        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (inputHorizontal > 0 && !facingRight)
        {
            Flip();
        }

        if (inputHorizontal < 0 && facingRight)
        {
            Flip();
        }
    }

    private void Movement()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rigidbody2d.velocity = new Vector2(-moveSpeed, rigidbody2d.velocity.y);
            animator.SetBool("isRunning", true);
            isRunning = true;
        }
        else
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                rigidbody2d.velocity = new Vector2(+moveSpeed, rigidbody2d.velocity.y);
                animator.SetBool("isRunning", true);
                isRunning = true;
            }
            else
            {
                rigidbody2d.velocity = new Vector2(0, rigidbody2d.velocity.y);
                animator.SetBool("isRunning", false);
                isRunning = false;
            }
        }
    }

    private void Jump()
    {
        rigidbody2d.velocity = Vector2.up * jumpVelocity;
    }

    bool IsGrounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, 1f, platformsLayerMask);
        return raycastHit2D.collider != null;
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
    }
}
