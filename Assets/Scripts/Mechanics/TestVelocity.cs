using UnityEngine;

public class TestVelocity : MonoBehaviour
{
    [SerializeField] private Vector2 strartingVelocity;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = strartingVelocity;
    }
}
