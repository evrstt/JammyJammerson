using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private LevelManager levelManager;

    private void Awake()
    {
        levelManager = FindFirstObjectByType<LevelManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerDeath playerDeath = other.GetComponent<PlayerDeath>();

        if(playerDeath != null)
        {
            levelManager.SetCheckpoint(transform);
        }
    }
}
