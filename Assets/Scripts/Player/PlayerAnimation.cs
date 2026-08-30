using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private void Update()
    {
        if(rb.linearVelocity.x > 0.1f)
        {
            spriteRenderer.flipX = false;
        }
        else if(rb.linearVelocity.x < -0.1f)
        {
            spriteRenderer.flipX = true;
        }
        
        float speed = Mathf.Abs(rb.linearVelocity.x);
        float verticalSpeed = Mathf.Abs(rb.linearVelocity.y);

        animator.SetFloat("Speed", speed);

        animator.SetBool("IsRunning", speed > 0.1f);

        animator.SetBool("IsJumping", verticalSpeed > 0.1f);
    }

    public void PlayHitAnimation()
    {
        animator.SetTrigger("IsHit");
    }
}