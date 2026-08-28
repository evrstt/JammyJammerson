using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance {get; private set;}

    [SerializeField] private string startingRoomScene;
    [SerializeField] private string startingSpawnID;

    private string targetSpawnID;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("LoadingScreen");
        StartCoroutine(LoadStartingRoom());
    }

    private IEnumerator LoadStartingRoom()
    {
        yield return null;

        LoadRoom(startingRoomScene, startingSpawnID);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
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
                
                if(player == null)
                {
                    Debug.LogError("No Player found in room: " + scene.name);
                    return;
                }
                    
                    player.transform.position = spawnPoint.transform.position;
                    LevelManager levelManager = FindFirstObjectByType<LevelManager>();
                    
                    if(levelManager == null)
                    {
                        Debug.LogError("No LevelManager found in room: " + scene.name);
                        return;
                    }
                    
                    levelManager.SetSpawnPoint(spawnPoint.transform);
                    Debug.Log("Respawn point set to: " + spawnPoint.SpawnID);
                    return;
            }
        }
            Debug.LogError("No RoomSpawnPoint found with ID: " + targetSpawnID + " in room: " + scene.name);
    }
}