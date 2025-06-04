using UnityEngine;

public class BounceBlock : MonoBehaviour
{
    public float bounceMultiplier = 1.5f;
    public LayerMask playerMask;

    private void Update()
    {
        Collider2D hit = Physics2D.OverlapBox(
            new Vector2(transform.position.x, transform.position.y + 0.55f),
            new Vector2(GetComponent<Collider2D>().bounds.size.x - 0.2f, 0.1f),
            0f,
            playerMask
        );

        if (hit != null)
        {
            Rigidbody2D playerRb = hit.attachedRigidbody;

            if (playerRb != null && playerRb.linearVelocity.y <= 0) // Only bounce if falling or stationary
            {
                float bounceForce = Mathf.Abs(playerRb.linearVelocity.y) * bounceMultiplier;
                playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, 0); // Reset vertical velocity
                playerRb.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(
            new Vector2(transform.position.x, transform.position.y + 0.55f),
            new Vector2(GetComponent<Collider2D>().bounds.size.x - 0.2f, 0.1f)
        );
    }
}