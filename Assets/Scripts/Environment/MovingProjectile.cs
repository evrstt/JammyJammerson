using UnityEngine;

public class MovingProjectile : MonoBehaviour
{
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

        if (Vector3.Distance(transform.position, nextPosition) < 0.01f)
        {
            transform.position = nextPosition;

                if (Vector3.Distance(nextPosition, pointA.position) < 0.01f)
                {
                    nextPosition = pointB.position;
                }
                else
                {
                    nextPosition = pointA.position;
                }
        }
    }
}
