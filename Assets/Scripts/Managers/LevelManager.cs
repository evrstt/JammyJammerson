using UnityEngine;

public class LevelManager : MonoBehaviour
{
[SerializeField] private Transform spawnPoint;

public Vector3 GetSpawnPosition()
    {
        return spawnPoint.position;
    }
}
