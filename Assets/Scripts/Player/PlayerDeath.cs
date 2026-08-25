using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
private Rigidbody2D rb;
private LevelManager levelManager;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        levelManager = FindAnyObjectByType<LevelManager>();
    }

    public void Die()
    {
        Respawn();
    }

    private void Respawn()
    {
        rb.linearVelocity = Vector2.zero;
        transform.position = levelManager.GetSpawnPosition();
    }
}
