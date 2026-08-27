using UnityEngine;

public class PauseMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private GameObject settingsPanel;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        pauseMenuPanel.SetActive(false);
        settingsPanel.SetActive(false);   
    }

    private void Start()
    {
        if(GameManager.Instance != null)
        {
            GameManager.Instance.PauseStateChanged += OnPauseStateChanged;
        }
    }

    private void OnDestroy()
    {
        if(GameManager.Instance != null)
        {
            GameManager.Instance.PauseStateChanged -= OnPauseStateChanged;
        }
    }

    private void OnPauseStateChanged(bool isPaused)
    {
        pauseMenuPanel.SetActive(isPaused);

        if(!isPaused)
        {
            settingsPanel.SetActive(false);
        }
    }

    public void ResumeGame()
    {
        GameManager.Instance.ResumeGame();
    }

    public void OpenSettings()
    {
        pauseMenuPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        pauseMenuPanel.SetActive(true);
    }
}
