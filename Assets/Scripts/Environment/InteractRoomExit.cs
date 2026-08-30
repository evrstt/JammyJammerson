using UnityEngine;
using UnityEngine.InputSystem;

public class InteractRoomExit : MonoBehaviour
{
    [SerializeField] private string targetScene;
    [SerializeField] private string targetSpawnID;

    [SerializeField] private GameObject interactPrompt;

    private bool playerInRange;

    private void Start()
    {
        if(interactPrompt != null)
        {
            interactPrompt.SetActive(false);
        }
    }

    private void Update()
    {
        if(playerInRange && Keyboard.current.eKey.wasPressedThisFrame)
        {
            SceneLoader.Instance.LoadRoom(
                targetScene,
                targetSpawnID
            );
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<PlayerControllerV1>() != null)
        {
            playerInRange = true;

            if(interactPrompt != null)
            {
                interactPrompt.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.GetComponent<PlayerControllerV1>() != null)
        {
            playerInRange = false;

            if(interactPrompt != null)
            {
                interactPrompt.SetActive(false);
            }
        }
    }
}