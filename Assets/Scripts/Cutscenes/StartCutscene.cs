using System.Collections;
using TMPro;
using UnityEngine;


public class StartCutscene : MonoBehaviour
{
    [SerializeField] private PlayerControllerV1 playerController;
    [SerializeField] private Rigidbody2D playerRb;

    [SerializeField] private CanvasGroup blackFade;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;

    [SerializeField] private Transform walkTarget;

    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private float typingSpeed = 0.03f;
    [SerializeField] private float walkSpeed = 5f;

    private void Start()
    {
        StartCoroutine(PlayCutscene());
    }

    private IEnumerator PlayCutscene()
    {
        playerController.enabled = false;
        playerRb.linearVelocity = Vector2.zero;

        yield return FadeFromBlack();

        dialoguePanel.SetActive(true);

        yield return TypeDialogue(
            "Rise and Shine, Testbot001. \n\n"
            + "It's time to start testing. Your Objective is to make it to the laboratory exit as quickly as possible... \n\n"
            + "Now get on with it... \n\n"
            + "Oh! \n\n"
            + "I almost forgot... Upon successful completion, you are free to enjoy the rest of your life... \n\n"
            + "doing whatever it is a sentient robot does... \n\n"
            + "Anywho, Happy testing!");

        yield return new WaitForSeconds(1f);
        dialoguePanel.SetActive(false);

        yield return WalkPlayerToExit();
    }

    private IEnumerator FadeFromBlack()
    {
        float elapsedTime = 0f;

        while(elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;

            blackFade.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
        }

        yield return null;
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

    private IEnumerator WalkPlayerToExit()
    {
        while(Vector2.Distance(playerRb.position, walkTarget.position) > 0.1f)
        {
            float direction = Mathf.Sign(walkTarget.position.x - playerRb.position.x);
            playerRb.linearVelocity = new Vector2(direction * walkSpeed, playerRb.linearVelocity.y);

            yield return new WaitForFixedUpdate();
            playerRb.linearVelocity = new Vector2(0f, playerRb.linearVelocity.y);
        }
    }
}
