using UnityEngine;
using UnityEngine.InputSystem;

public class PauseInput : MonoBehaviour
{
    public void OnPause(InputValue value)
    {
        if(value.isPressed)
        {
            GameManager.Instance.TogglePause();
        }
    }
}
