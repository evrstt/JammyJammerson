using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance{get; private set;}

    public float MasterVolume {get; private set;}
    public float MusicVolume {get; private set;}
    public float SFXVolume {get; private set;}

    private const string MasterVolumeKey = "MasterVolume";
    private const string MusicVolumeKey = "MusicVolume";
    private const string SFXVolumeKey = "SFXVolume";

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);
        LoadSettings();
    }

    private void Start()
    {
        ApplyAudioSettings();

        Debug.Log("Master Volume: " + MasterVolume);
        Debug.Log("Music Volume: " + MusicVolume);
        Debug.Log("SFX Volume: " + SFXVolume);

    }

    public void SetMasterVolume(float volume)
    {
        MasterVolume = volume;

        AudioManager.instance.SetMasterVolume(volume);

        PlayerPrefs.SetFloat(MasterVolumeKey, volume);
        PlayerPrefs.Save();
    }

    public void SetMusicVolume(float volume)
    {
        MusicVolume = volume;

        AudioManager.instance.SetMusicVolume(volume);

        PlayerPrefs.SetFloat(MusicVolumeKey, volume);
        PlayerPrefs.Save();
    }

    public void SetSFXVolume(float volume)
    {
        SFXVolume = volume;

        AudioManager.instance.SetSFXVolume(volume);

        PlayerPrefs.SetFloat(SFXVolumeKey, volume);
        PlayerPrefs.Save();
    }

    private void LoadSettings()
    {
        MasterVolume = PlayerPrefs.GetFloat(MasterVolumeKey, 1f);
        MusicVolume = PlayerPrefs.GetFloat(MusicVolumeKey, 1f);
        SFXVolume = PlayerPrefs.GetFloat(SFXVolumeKey, 1f);
    }

    public void ApplyAudioSettings()
    {
    AudioManager.instance.SetMasterVolume(MasterVolume);
    AudioManager.instance.SetMusicVolume(MusicVolume);
    AudioManager.instance.SetSFXVolume(SFXVolume);
    }
}
