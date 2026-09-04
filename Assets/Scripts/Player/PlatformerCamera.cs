using UnityEngine;

public class PlatformerCamera : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform target;
    [SerializeField] private Rigidbody2D targetRb;

    [Header("Camera Postion")]
    [SerializeField] private Vector2 offset;

    [Header("Smoothing")]
    [SerializeField] private float horizontalSmoothTime = 0.15f;
    [SerializeField] private float verticalSmoothTime = 0.25f;

    [Header("Look Ahead")]
    [SerializeField] private float lookAheadDistance = 1.5f;
    [SerializeField] private float lookAheadSpeed = 3f;

    private float horizontalVelocity;
    private float verticalVelocity;
    private float currentLookAhead;

    private void LateUpdate()
    {
        if(target == null)
        {
            return;
        }

        float moveDirection = 0f;

        if(targetRb != null && Mathf.Abs(targetRb.linearVelocity.x) > 0.1f)
        {
            moveDirection = Mathf.Sign(targetRb.linearVelocity.x);
        }

        float targetLookAhead = moveDirection * lookAheadDistance;
        currentLookAhead = Mathf.MoveTowards(currentLookAhead, targetLookAhead, lookAheadSpeed * Time.deltaTime);

        float targetX = target.position.x + offset.x + currentLookAhead;
        float targetY = target.position.y + offset.y;
        float newX = Mathf.SmoothDamp(transform.position.x, targetX, ref horizontalVelocity, horizontalSmoothTime);
        float newY = Mathf.SmoothDamp(transform.position.y, targetY, ref verticalVelocity, verticalSmoothTime);

        transform.position = new Vector3(newX, newY, transform.position.z);
    }
}
