using UnityEngine;

public class RoomExit : MonoBehaviour
{
    [SerializeField] private string targetScene;
    [SerializeField] private string targetSpawnID;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<PlayerControllerV1>() != null)
        {
            SceneLoader.Instance.LoadRoom(targetScene, targetSpawnID);
        }
    }
}
