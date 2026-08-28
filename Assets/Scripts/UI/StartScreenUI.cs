using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;


public class StartScreenUI : MonoBehaviour
{
    [SerializeField] private GameObject startScreenPanel;
    [SerializeField] private GameObject mainMenuPanel;

    [SerializeField] private AudioClip menuMusic;

    private IDisposable buttonPressListener;
    private bool waitingForInput;

    private void Start()
    {
        startScreenPanel.SetActive(true);
        mainMenuPanel.SetActive(false);

        if(AudioManager.instance != null)
        {
            AudioManager.instance.PlayMusic(menuMusic);
        }

        waitingForInput = true;

        buttonPressListener = InputSystem.onAnyButtonPress.CallOnce(control => ContinueToMainMenu());
    }

    private void ContinueToMainMenu()
    {
        if(!waitingForInput)
        {
            return;
        }

        waitingForInput = false;

        startScreenPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
        buttonPressListener?.Dispose();
        buttonPressListener = null;
    }

    private void OnDestroy()
    {
        buttonPressListener?.Dispose();
    }
}
