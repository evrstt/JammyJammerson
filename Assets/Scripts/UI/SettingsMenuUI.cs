using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuUI : MonoBehaviour
{
    private enum PreviousMenu
    {
        MainMenu,
        PauseMenu
    }
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;

    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject pauseMenuPanel;

    private PreviousMenu previousMenu;

    private void OnEnable()
    {
        if(SettingsManager.Instance == null)
        {
            return;
        }

        masterVolumeSlider.SetValueWithoutNotify(SettingsManager.Instance.MasterVolume);
        musicVolumeSlider.SetValueWithoutNotify(SettingsManager.Instance.MusicVolume);
        sfxVolumeSlider.SetValueWithoutNotify(SettingsManager.Instance.SFXVolume);
    }

    public void OpenFromMainMenu()
    {
        previousMenu = PreviousMenu.MainMenu;
        mainMenuPanel.SetActive(false);
        pauseMenuPanel.SetActive(false);

        gameObject.SetActive(true);
    }

    public void OpenFromPauseMenu()
    {
        previousMenu = PreviousMenu.PauseMenu;
        mainMenuPanel.SetActive(false);
        pauseMenuPanel.SetActive(false);

        gameObject.SetActive(true);
    }

    public void CloseSettings()
    {
        gameObject.SetActive(false);

        if(previousMenu == PreviousMenu.MainMenu)
        {
            mainMenuPanel.SetActive(true);
        }
        else
        {
            pauseMenuPanel.SetActive(true);
        }
    }

    public void SetMasterVolume(float volume)
    {
        SettingsManager.Instance.SetMasterVolume(volume);
    }

    public void SetMusicVolume(float volume)
    {
        SettingsManager.Instance.SetMusicVolume(volume);
    }

    public void SetSFXVolume(float volume)
    {
        SettingsManager.Instance.SetSFXVolume(volume);
    }
}
