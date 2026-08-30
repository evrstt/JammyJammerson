using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rb;

    private void Update()
    {
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