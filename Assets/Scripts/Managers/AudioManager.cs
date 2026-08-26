using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance{get; private set;}

    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public void PlayMusic(AudioClip musicClip)
    {
        if(musicSource.clip == musicClip && musicSource.isPlaying)
        {
            return;
        }

        musicSource.clip = musicClip;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void PlaySFX(AudioClip sfxClip)
    {
        sfxSource.PlayOneShot(sfxClip);
    }

    public void SetMasterVolume(float volume)
    {
        SetMixerVolume("MasterVolume", volume);
    }

    public void SetMusicVolume(float volume)
    {
        SetMixerVolume("MusicVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        SetMixerVolume("SFXVolume", volume);
    }

    private void SetMixerVolume(string parameterName, float volume)
    {
        if(volume <= 0)
        {
            audioMixer.SetFloat(parameterName, -80f);
            return;
        }

        float volumeInDecibels = Mathf.Log10(volume) * 20f;

        audioMixer.SetFloat(parameterName, volumeInDecibels);
    }
}
