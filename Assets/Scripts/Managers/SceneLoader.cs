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

    private bool isTransitioning;
    private bool isPreloadingNextLevel;

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

        yield return StartCoroutine(PreloadNextLevel());

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
        if(isTransitioning)
        {
            return;
        }

        StartCoroutine(TransitionToNextLevel());
    }

    private IEnumerator TransitionToNextLevel()
    {
        if(currentLevelIndex + 1 >= levelSceneNames.Length)
        {
            Debug.Log("No more Levels, Game Complete");
            yield break;
        }

        isTransitioning = true;

        if(!nextLevelLoaded && !isPreloadingNextLevel)
        {
            yield return StartCoroutine(PreloadNextLevel());
        }

        while(isPreloadingNextLevel)
        {
            yield return null;
        }

        if(!nextLevelLoaded || nextLevelRoot == null)
        {
            Debug.LogError("Next level failed to preload.");
            isTransitioning = false;
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
        nextLevelScene = default;
        nextLevelRoot = null;

        AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync(previousScene);

        while(unloadOperation != null && !unloadOperation.isDone)
        {
            yield return null;
        }

        StartCoroutine(PreloadNextLevel());

        isTransitioning = false;
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

    private IEnumerator PreloadNextLevel()
    {
        if(currentLevelIndex + 1 >= levelSceneNames.Length)
        {
            nextLevelLoaded = false;
            nextLevelScene = default;
            nextLevelRoot = null;
            yield break;
        }

        isPreloadingNextLevel = true;

        string nextSceneName = levelSceneNames[currentLevelIndex +1];

        yield return LoadLevel(nextSceneName);

        nextLevelScene = SceneManager.GetSceneByName(nextSceneName);
        nextLevelRoot = GetLevelRoot(nextLevelScene);

        nextLevelLoaded = true;
        isPreloadingNextLevel = false;
    }
}
