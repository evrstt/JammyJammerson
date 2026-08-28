using UnityEngine;

public class ConfirmationMenuUI : MonoBehaviour
{
    private enum ConfirmationAction
    {
        QuitGame,
        ReturnToMainMenu
    }

    private enum PreviousMenu
    {
        MainMenu,
        PauseMenu
    }

    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject confirmationPanel;

    private ConfirmationAction confirmationAction;
    private PreviousMenu previousMenu;

    public void OpenQuitConfirmation()
    {
        confirmationAction = ConfirmationAction.QuitGame;
        previousMenu = PreviousMenu.MainMenu;

        mainMenuPanel.SetActive(false);
        confirmationPanel.SetActive(true);
    }

    public void OpenMainMenuConfirmation()
    {
        confirmationAction = ConfirmationAction.ReturnToMainMenu;
        previousMenu = PreviousMenu.PauseMenu;

        pauseMenuPanel.SetActive(false);
        confirmationPanel.SetActive(true);
    }

    public void ConfirmYes()
    {
        if(confirmationAction == ConfirmationAction.QuitGame)
        {
            Application.Quit();

            Debug.Log("Quit Game");
        }
        else
        {
            confirmationPanel.SetActive(false);
            pauseMenuPanel.SetActive(false);
            settingsPanel.SetActive(false);

            GameManager.Instance.ResumeGame();
            mainMenuPanel.SetActive(true);
            SceneLoader.Instance.LoadMainMenu();
        }
    }

    public void ConfirmNo()
    {
        confirmationPanel.SetActive(false);

        if(previousMenu == PreviousMenu.MainMenu)
        {
            mainMenuPanel.SetActive(true);
        }
        else
        pauseMenuPanel.SetActive(true);
    }
}
