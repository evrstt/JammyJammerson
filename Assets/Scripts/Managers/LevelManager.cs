using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Transform startingSpawnPoint;
    private Transform currentSpawnPoint;
    private bool isLevelComplete;

    private void Awake()
    {
        currentSpawnPoint = startingSpawnPoint;
    }

    public Vector3 GetSpawnPosition()
    {
        return currentSpawnPoint.position;
    }

    public void SetCheckpoint(Transform checkpoint)
    {
        currentSpawnPoint = checkpoint;
    }

    public void CompleteLevel()
    {
        if(isLevelComplete)
        {
            return;
        }

        isLevelComplete = true;

        Debug.Log("Level Complete!");

        SceneLoader.Instance.LoadNextLevel();
    }
}
