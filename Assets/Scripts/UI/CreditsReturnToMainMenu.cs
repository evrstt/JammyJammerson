using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsReturnToMainMenu : MonoBehaviour
{
    [SerializeField] private float returnDelay = 10f;

    private void Start()
    {
        StartCoroutine(ReturnToMainMenu());
    }

    private IEnumerator ReturnToMainMenu()
    {
        yield return new WaitForSecondsRealtime(returnDelay);

        Time.timeScale = 1f;

        if(GameManager.Instance != null)
        {
            Destroy(GameManager.Instance.gameObject);
        }

        if(SceneLoader.Instance != null)
        {
            Destroy(SceneLoader.Instance.gameObject);
        }

        if(AudioManager.instance != null)
        {
            Destroy(AudioManager.instance.gameObject);
        }

        if(SettingsManager.Instance != null)
        {
            Destroy(SettingsManager.Instance.gameObject);
        }

        if(PersistentUI.Instance != null)
        {
            Destroy(PersistentUI.Instance.gameObject);
        }

        yield return null;

        SceneManager.LoadScene("MainMenu");
    }
}