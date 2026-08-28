using UnityEngine;

public class MovingProjectile : MonoBehaviour
{
    // this is basically copy paste of the platform script so idk maybe it should just be one script but im making it 2 so we can edit easier
    [SerializeField] private float speed; 
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;

    private Vector3 nextPosition;

    void Start()
    {
        nextPosition = pointB.position;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, nextPosition, speed * Time.deltaTime);

        if (transform.position == nextPosition)
            {
                nextPosition = (nextPosition == pointA.position) ? pointB.position : pointA.position;
            }
    }
}
