using UnityEngine;

public class Hazard : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerDeath playerDeath = other.GetComponent<PlayerDeath>();

        if(playerDeath != null)
        {
            playerDeath.Die();
        }
    }
}
