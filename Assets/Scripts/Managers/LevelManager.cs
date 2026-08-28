using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private Transform currentSpawnPoint;

    public Vector3 GetSpawnPosition()
    {
        return currentSpawnPoint.position;
    }

    public void SetSpawnPoint(Transform spawnPoint)
    {
        currentSpawnPoint = spawnPoint;
    }

    public void SetCheckpoint(Transform checkpoint)
    {
        currentSpawnPoint = checkpoint;
    }
}
