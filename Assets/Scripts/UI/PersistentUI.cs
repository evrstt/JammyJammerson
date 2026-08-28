using UnityEngine;

public class PersistentUI : MonoBehaviour
{
    public static PersistentUI Instance {get; private set;}

    [SerializeField] private GameObject eventSystem;

    private void Awake()
    {
        if(Instance != null && Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);

        eventSystem.SetActive(true);
    }
}
