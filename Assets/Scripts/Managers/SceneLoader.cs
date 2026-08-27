using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance {get; private set;}

    private string targetSpawnID;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public void LoadRoom(string sceneName, string spawnID)
    {
        targetSpawnID = spawnID;

        SceneManager.sceneLoaded += OnRoomLoaded;
        SceneManager.LoadScene(sceneName);
    }

    private void OnRoomLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnRoomLoaded;

        RoomSpawnPoint[] spawnPoints = FindObjectsByType<RoomSpawnPoint>(FindObjectsSortMode.None);

        foreach(RoomSpawnPoint spawnPoint in spawnPoints)
        {
            if(spawnPoint.SpawnID == targetSpawnID)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                player.transform.position = spawnPoint.transform.position;
                break;
            }
        }
    }

}