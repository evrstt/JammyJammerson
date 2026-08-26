using UnityEngine;

public class PauseMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private GameObject settingsPanel;

    private void OnEnable()
    {
        GameManager.Instance.PauseStateChanged += OnPauseStateChanged;
    }

    private void OnDisable()
    {
        GameManager.Instance.PauseStateChanged -= OnPauseStateChanged;
    }

    private void Start()
    {
        pauseMenuPanel.SetActive(false);
        settingsPanel.SetActive(false);
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
