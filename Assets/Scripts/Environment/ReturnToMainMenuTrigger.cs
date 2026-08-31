using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMainMenuTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<PlayerControllerV1>() != null)
        {
            SceneManager.LoadScene("MainMenu");

            StartScreenUI startScreen =
                FindFirstObjectByType<StartScreenUI>();

            if(startScreen != null)
            {
                startScreen.ShowMainMenu();
            }
        }
    }
}