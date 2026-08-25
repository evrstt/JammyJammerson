using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerV1 : MonoBehaviour
{
[Header("Movement")]
[SerializeField] private float moveSpeed = 10f;
[SerializeField] private float jumpForce = 14f;

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

    public void OnJump(InputValue value)
    {
        if(value.isPressed && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }
}
