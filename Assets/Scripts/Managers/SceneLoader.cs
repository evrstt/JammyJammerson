using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance {get; private set;}

    [SerializeField] private string[] levelSceneNames;

    private int currentLevelIndex;
    
    private Scene currentLevelScene;
    private Scene nextLevelScene;

    private GameObject currentLevelRoot;
    private GameObject nextLevelRoot;

    private bool nextLevelLoaded;

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

    private void Start()
    {
        StartCoroutine(LoadStartingLevels());
    }

    private IEnumerator LoadStartingLevels()
    {
        currentLevelIndex = 0;

        yield return LoadLevel(levelSceneNames[currentLevelIndex]);

        currentLevelScene = SceneManager.GetSceneByName(levelSceneNames[currentLevelIndex]);
        currentLevelRoot = GetLevelRoot(currentLevelScene);

        if(currentLevelIndex + 1 < levelSceneNames.Length)
        {
            yield return LoadLevel(levelSceneNames[currentLevelIndex +1]);
            nextLevelScene = SceneManager.GetSceneByName(levelSceneNames[currentLevelIndex + 1]);
            nextLevelRoot = GetLevelRoot(nextLevelScene);
            nextLevelLoaded = true;
        }

        currentLevelRoot.SetActive(true);
        SceneManager.SetActiveScene(currentLevelScene);
        Scene loadingScene = SceneManager.GetSceneByName("LoadingScreen");

        if(loadingScene.isLoaded)
        {
            SceneManager.UnloadSceneAsync(loadingScene);
        }
    }

    private IEnumerator LoadLevel(string sceneName)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        while(!loadOperation.isDone)
        {
            yield return null;
        }
    }

    public void LoadNextLevel()
    {
        StartCoroutine(TransitionToNextLevel());
    }

    private IEnumerator TransitionToNextLevel()
    {
        if(!nextLevelLoaded)
        {
            yield break;
        }

        currentLevelRoot.SetActive(false);
        nextLevelRoot.SetActive(true);
        SceneManager.SetActiveScene(nextLevelScene);
        Scene previousScene = currentLevelScene;

        currentLevelIndex++;
        
        currentLevelScene = nextLevelScene;
        currentLevelRoot = nextLevelRoot;

        nextLevelLoaded = false;

        SceneManager.UnloadSceneAsync(previousScene);

        if(currentLevelIndex + 1 < levelSceneNames.Length)
        {
            yield return LoadLevel(levelSceneNames[currentLevelIndex +1]);
            nextLevelScene = SceneManager.GetSceneByName(levelSceneNames[currentLevelIndex +1]);

            nextLevelRoot = GetLevelRoot(nextLevelScene);
            nextLevelLoaded = true;
        }
    }

    private GameObject GetLevelRoot(Scene scene)
    {
        GameObject[] rootObjects = scene.GetRootGameObjects();

        foreach(GameObject rootObject in rootObjects)
        {
            if(rootObject.GetComponent<LevelRoot>() != null)
            {
                return rootObject;
            }
        }

        return null;
    }
}
