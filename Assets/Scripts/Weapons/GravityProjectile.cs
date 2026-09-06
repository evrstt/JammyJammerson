using UnityEngine;

public class GravityProjectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 12f;
    [SerializeField] private GameObject blackHolePrefab;

    private Rigidbody2D rb;
    private Vector2 moveDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Initialize(Vector2 direction)
    {
        moveDirection = direction.normalized;
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = moveDirection * moveSpeed;
    }

    public GameObject ActivateBlackHole()
    {
        GameObject blackHole = Instantiate(blackHolePrefab, transform.position, Quaternion.identity);

        Destroy(gameObject);

        return blackHole;
    }
}
