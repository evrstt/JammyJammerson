using UnityEngine;

public class HowToPlayUI : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject howToPlayPanel;

    public void CloseHowToPlay()
    {
        howToPlayPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
}
