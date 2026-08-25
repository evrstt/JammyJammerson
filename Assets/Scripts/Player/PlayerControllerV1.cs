using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerV1 : MonoBehaviour
{
[Header("Movement")]
[SerializeField] private float moveSpeed = 10f;
[SerializeField] private float jumpForce = 14f;
[SerializeField] private int jumpLimit = 2;
[SerializeField] private int currentNumberOfJumps = 0;

[Header("References")]
[SerializeField] private Transform groundCheck;
[SerializeField] private float groundCheckRadius = 0.2f;
[SerializeField] private LayerMask groundLayer;

private Rigidbody2D rb;

private Vector2 moveInput;
private bool isGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }


    /// <summary>
    /// The player can only jump if they haven't reached the jumpLimit, or if they're grounded.
    /// If the player isGrounded, the currentNumberOfJumps is reset, so they can't double jump infinitely.
    /// </summary>
    /// <param name="value"></param>
    public void OnJump(InputValue value)
    {
        if(value.isPressed && (isGrounded || currentNumberOfJumps < jumpLimit))
        {
            if(isGrounded)
            {
                currentNumberOfJumps = 0;
            }
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            currentNumberOfJumps++;
        }
    }
}
