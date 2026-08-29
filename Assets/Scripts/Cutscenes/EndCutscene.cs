using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndCutscene : MonoBehaviour
{
    [SerializeField] private PlayerControllerV1 playerController;
    [SerializeField] private Rigidbody2D playerRb;
    [SerializeField] private Transform walkTarget;

    [SerializeField] private CanvasGroup blackFade;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;

    [SerializeField] private AudioClip endAmbience;
    [SerializeField] private AudioClip deathSound;

    [SerializeField] private float startDelay = 2f;
    [SerializeField] private float fadeDuration = 3f;
    [SerializeField] private float typingSpeed = 0.06f;
    [SerializeField] private float dialogueWaitTime = 3f;
    [SerializeField] private float deathWaitTime = 3f;
    [SerializeField] private float walkSpeed = 2f;

    [SerializeField] private string nextSceneName;

    private void Start()
    {
        StartCoroutine(PlayCutscene());
    }

    private IEnumerator PlayCutscene()
    {
        playerController.enabled = false;
        playerRb.linearVelocity = Vector2.zero;

        if(AudioManager.instance != null)
        {
            AudioManager.instance.PlayMusic(endAmbience);
        }

        yield return new WaitForSeconds(startDelay);

        yield return FadeFromBlack();

        dialoguePanel.SetActive(true);

        int deathCount = 0;

        if(GameManager.Instance != null)
        {
            deathCount = GameManager.Instance.TotalDeaths;
        }

        int testBotNumber =1 + deathCount;
        string testBotID = testBotNumber.ToString("000");

        string dialogue = "Congratulations TestBot" + testBotID + ", you've completed the test.... \n\n"
                            + "Please enjoy whatever it is a robot would do for the remainder for you life... \n\n"
                            + "Oh! \n\n"
                            + "One last thing... \n\n"
                            + "Happy Testing!";
        
        yield return TypeDialogue(dialogue);

        yield return new WaitForSeconds(dialogueWaitTime);

        dialoguePanel.SetActive(false);

        yield return WalkPlayerOffScreen();

        CutToBlack();

        if(AudioManager.instance != null)
        {
            AudioManager.instance.PlaySFX(deathSound);
        }

        yield return new WaitForSeconds(deathWaitTime);

        SceneManager.LoadScene(nextSceneName);
    }

    private IEnumerator FadeFromBlack()
    {
        blackFade.gameObject.SetActive(true);
        blackFade.alpha = 1f;

        float elapsedTime = 0f;

        while(elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;

            blackFade.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);

            yield return null;
        }

        blackFade.alpha = 0f;
        blackFade.gameObject.SetActive(false);
    }

    private IEnumerator TypeDialogue(string dialogue)
    {
        dialogueText.text = "";

        foreach(char letter in dialogue)
        {
            dialogueText.text += letter;

            yield return new WaitForSeconds(typingSpeed);
        }
    }

    private IEnumerator WalkPlayerOffScreen()
    {
        while(Vector2.Distance(playerRb.position, walkTarget.position) > 0.1f);
        {
            float direction = Mathf.Sign(walkTarget.position.x - playerRb.position.x);
            playerRb.linearVelocity = new Vector2(direction * walkSpeed, playerRb.linearVelocity.y);
             yield return new WaitForFixedUpdate();
        }
        
        playerRb.linearVelocity = new Vector2(0f, playerRb.linearVelocity.y);
    }

    private void CutToBlack()
    {
        blackFade.gameObject.SetActive(true);
        blackFade.alpha = 1f;
    }
}
