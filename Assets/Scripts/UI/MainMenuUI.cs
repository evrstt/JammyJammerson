using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject settingsPanel;

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

    public void QuitGame()
    {
        Application.Quit();
        
        Debug.Log("Quit Game");
    }
}
