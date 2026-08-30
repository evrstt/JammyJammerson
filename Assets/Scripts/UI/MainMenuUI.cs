using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject howToPlayPanel;

    public void PlayGame()
    {
        mainMenuPanel.SetActive(false);

        if(AudioManager.instance != null)
        {
            AudioManager.instance.StopMusic();
        }
        SceneLoader.Instance.StartGame();
    }

    public void OpenSettings()
    {
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void OpenHowToPlay()
    {
        mainMenuPanel.SetActive(false);
        howToPlayPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
        
        Debug.Log("Quit Game");
    }
}
