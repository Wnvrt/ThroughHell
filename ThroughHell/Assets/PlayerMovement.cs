using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed;
    private float moveInput;
    public bool isGrounded;
    private Rigidbody2D rb;
    public LayerMask groundMask;

    public bool canJump = true;
    public float jumpValue = 0.0f;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        if ((!((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && isGrounded)) || !isGrounded) {
            rb.linearVelocity = new Vector2(moveInput * walkSpeed, rb.linearVelocity.y);
        }

        isGrounded = Physics2D.OverlapBox(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 0.4f), new Vector2(0.6f, 0.4f), 0f, groundMask);

        if((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && isGrounded && canJump)
        {
            jumpValue += 0.1f;
        }

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && isGrounded && canJump)
        {
            rb.linearVelocity = new Vector2(0.0f, rb.linearVelocity.y);
        }

        if (jumpValue >= 10f && isGrounded)
        {
            float tempx = moveInput * walkSpeed;
            float tempy = jumpValue;
            rb.linearVelocity = new Vector2(tempx,tempy);
            Invoke("ResetJump",0.2f);
        }

        if (Input.GetKeyUp(KeyCode.W)|| Input.GetKeyUp(KeyCode.UpArrow))
        {
            if (isGrounded)
            {
                rb.linearVelocity = new Vector2(moveInput * walkSpeed, jumpValue);
                jumpValue = 0;
            }
            canJump = true;
        }
    }

    private void ResetJump()
    {
        canJump = false;
        jumpValue = 0;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 0.4f), new Vector2(0.6f, 0.2f));
    }
}
