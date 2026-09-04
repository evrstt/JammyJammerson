using UnityEngine;

public class BlackHole : MonoBehaviour
{
    [SerializeField] private float pullRadius = 5f;
    [SerializeField] private float pullStrength = 25f;

    [SerializeField] private LayerMask affectedLayers;

    private void FixedUpdate()
    {
        Collider2D[] affectedObjects = Physics2D.OverlapCircleAll(transform.position, pullRadius, affectedLayers);

        foreach(Collider2D affectedObject in affectedObjects)
        {
            Rigidbody2D affectedRb = affectedObject.attachedRigidbody;

            if(affectedRb == null)
            {
                continue;
            }

            if(affectedRb.bodyType != RigidbodyType2D.Dynamic)
            {
                continue;
            }

            Vector2 directionToBlackHole = (Vector2)transform.position - affectedRb.position;
            float distance = directionToBlackHole.magnitude;
            
            if(distance <= 0.5f)
            {
                continue;
            }

            float pullPercent = 1f - Mathf.Clamp01(distance/pullRadius);
            float currentPullStrength = pullStrength * pullPercent;
            affectedRb.AddForce(directionToBlackHole.normalized * currentPullStrength, ForceMode2D.Force);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, pullRadius);
    }

}
