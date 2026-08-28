using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform[] points;
    [SerializeField] private int startingPoint;
    private int pointIndex;
    
    void Start()
    {
        transform.position = points[startingPoint].position;
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, points[pointIndex].position) < 0.02f)
        {
            pointIndex++;
            if (pointIndex == points.Length)
            {
                pointIndex = 0;
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, points[pointIndex].position, speed * Time.deltaTime);
    }
}
