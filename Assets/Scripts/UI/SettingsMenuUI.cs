using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuUI : MonoBehaviour
{
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;

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

    public void SetMasterVolume(float volume)
    {
        SettingsManager.Instance.SetMasterVolume(volume);
    }

    public void SetMusicVolume(float volume)
    {
        SettingsManager.Instance.SetMasterVolume(volume);
    }

    public void SetSFXVolume(float volume)
    {
        SettingsManager.Instance.SetSFXVolume(volume);
    }
}
