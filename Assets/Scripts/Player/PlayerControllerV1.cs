using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerV1 : MonoBehaviour
{
[Header("Movement")]
[SerializeField] private float moveSpeed = 10f;
[SerializeField] private float jumpForce = 14f;
[SerializeField] private float fallForce = 20f;
[SerializeField] private float jumpCutMultiplier = 0.5f;
[SerializeField] private float coyoteTime = 0.1f;
[SerializeField] private float jumpBufferTime = 0.1f;
[SerializeField] private float acceleration = 120f;
[SerializeField] private float deceleration = 150f;

[Header("References")]
[SerializeField] private Transform groundCheck;
[SerializeField] private float groundCheckRadius = 0.2f;
[SerializeField] private LayerMask groundLayer;

private Rigidbody2D rb;

private Vector2 moveInput;
private bool isGrounded;
private float coyoteTimeCounter;
private float jumpBufferCounter;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if(isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        jumpBufferCounter -= Time.deltaTime;

        if(jumpBufferCounter > 0 && coyoteTimeCounter > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);

            jumpBufferCounter = 0;
            coyoteTimeCounter = 0;
        }
    }

    private void FixedUpdate()
    {
        float targetSpeed = moveInput.x * moveSpeed;
        float accelerationRate;

        if(moveInput.x != 0)
        {
            accelerationRate = acceleration;
        }
        else
        {
            accelerationRate = deceleration;
        }

        float newXVelocity = Mathf.MoveTowards(rb.linearVelocity.x, targetSpeed, accelerationRate * Time.fixedDeltaTime);
        rb.linearVelocity = new Vector2(newXVelocity, rb.linearVelocity.y);

        if(rb.linearVelocity.y < 0)
        {
            rb.AddForce(Vector2.down * fallForce);
        }
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnJump(InputValue value)
    {
        if(value.isPressed)
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else if(!value.isPressed && rb.linearVelocity.y > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * jumpCutMultiplier);
        }
    }
}
