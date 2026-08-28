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
        if(isPaused)
        {
            pauseMenuPanel.SetActive(true);
            settingsPanel.SetActive(false);
        }
        else
        {
            pauseMenuPanel.SetActive(false);
            settingsPanel.SetActive(false);
        }
    }

    public void ResumeGame()
    {
        GameManager.Instance.ResumeGame();
    }
}
