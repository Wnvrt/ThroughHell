using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed;
    private float moveInput;
    public bool isGrounded, isOnIce, isOnGround, isOnBounce;
    private Rigidbody2D rb;
    public LayerMask groundMask, iceMask, bounceMask;
    public PhysicsMaterial2D playerDefault, playerGround, playerIce;

    public float jumpValue = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpValue = 5f;
    }

    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        // Check grounded state
        isOnGround = Physics2D.OverlapBox(
            new Vector2(transform.position.x, transform.position.y - 0.4f),
            new Vector2(0.45f, 0.4f),
            0f,
            groundMask
        );
        isOnIce = Physics2D.OverlapBox(
            new Vector2(transform.position.x, transform.position.y - 0.4f),
            new Vector2(0.45f, 0.4f),
            0f,
            iceMask
        );
        isOnBounce = Physics2D.OverlapBox(
            new Vector2(transform.position.x, transform.position.y - 0.4f),
            new Vector2(0.45f, 0.4f),
            0f,
            bounceMask
        );

        isGrounded = isOnGround || isOnIce;

        // Apply physics material
        if (isOnIce)
        {
            rb.sharedMaterial = playerIce;
        }
        else if (isOnGround || isOnBounce)
        {
            rb.sharedMaterial = playerGround;
        }
        else
        {
            rb.sharedMaterial = playerDefault;
        }

        if (isOnBounce && rb.linearVelocity.y==0)
        {
            isGrounded = true;
        }

        // Jump key state
        bool isJumpHeld = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);

        // Movement logic
        if (isGrounded && !isOnIce && isJumpHeld)
        {
            // On normal ground, holding jump stops movement
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
        }
        else if (isOnIce)
        {
            if (!isJumpHeld)
            {
                if (moveInput != 0)
                {
                    float targetSpeed = moveInput * walkSpeed;
                    float iceAcceleration = 10f; // tweak for how fast speed changes on ice
                    float smoothedX = Mathf.MoveTowards(rb.linearVelocity.x, targetSpeed, iceAcceleration * Time.deltaTime);
                    rb.linearVelocity = new Vector2(smoothedX, rb.linearVelocity.y);
                }
                // else: preserve sliding (do nothing)
            }
            else if (moveInput == 0)
            {
                // Holding jump but no movement input ? preserve momentum
                // Do nothing
            }
            else
            {
                // Holding jump + movement input on ice ? do nothing to preserve slide
                // Do nothing
            }
        }
        else if (isGrounded && !isJumpHeld)
        {
            // On ground and not on ice and not holding jump
            rb.linearVelocity = new Vector2(moveInput * walkSpeed, rb.linearVelocity.y);
        }
        else if (!isGrounded && moveInput != 0)
        {
            // Responsive air control
            float targetSpeed = moveInput * walkSpeed;
            float acceleration = 30f; // Higher = more responsive
            float smoothedX = Mathf.MoveTowards(rb.linearVelocity.x, targetSpeed, acceleration * Time.deltaTime);
            rb.linearVelocity = new Vector2(smoothedX, rb.linearVelocity.y);
        }

        // Jump charging
        if (isJumpHeld && isGrounded)
        {
            jumpValue += 0.05f;
        }

        // Cancel vertical velocity at jump start
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
        }

        // Trigger jump when charged enough
        if (jumpValue >= 10f && isGrounded)
        {
            rb.linearVelocity = new Vector2(moveInput * walkSpeed, jumpValue);
            Invoke(nameof(ResetJump), 0.2f);
        }

        // On jump release
        if ((Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow)) && isGrounded)
        {
            rb.linearVelocity = new Vector2(moveInput * walkSpeed, jumpValue);
            jumpValue = 5f;
        }
    }

    private void ResetJump()
    {
        jumpValue = 5f;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(
            new Vector2(transform.position.x, transform.position.y - 0.4f),
            new Vector2(0.45f, 0.2f)
        );
    }
}