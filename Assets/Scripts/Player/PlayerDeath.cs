using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private AudioClip deathSound;
    private Rigidbody2D rb;
    private LevelManager levelManager;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        levelManager = FindAnyObjectByType<LevelManager>();
    }

    public void Die()
    {
        if(GameManager.Instance != null)
        {
            GameManager.Instance.RegisterDeath();
        }

        if(AudioManager.instance != null)
        {
            AudioManager.instance.PlaySFX(deathSound);
        }
        Respawn();
    }

    private void Respawn()
    {
        rb.linearVelocity = Vector2.zero;
        transform.position = levelManager.GetSpawnPosition();
    }
}
