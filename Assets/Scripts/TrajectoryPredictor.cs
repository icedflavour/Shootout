using UnityEngine;
using System.Collections.Generic;

public class TrajectoryPredictor : MonoBehaviour
{
    public LineRenderer predictionRenderer;
    public int maxBounces = 1;
    public int maxSteps = 100;
    public float stepLength = 0.5f;
    public LayerMask collisionMask;

    public void Predict(Vector3 startPos, Vector3 direction, float initialSpeed)
    {
        List<Vector3> points = new List<Vector3>();
        Vector3 currentPos = startPos;
        Vector3 velocity = direction.normalized * initialSpeed;

        points.Add(currentPos);

        for (int bounce = 0; bounce <= maxBounces; bounce++)
        {
            for (int step = 0; step < maxSteps; step++)
            {
                Vector3 nextPos = currentPos + velocity.normalized * stepLength;
                if (Physics.Linecast(currentPos, nextPos, out RaycastHit hit, collisionMask))
                {
                    points.Add(hit.point);
                    velocity = Vector3.Reflect(velocity, hit.normal);
                    currentPos = hit.point;
                    break;
                }
                else
                {
                    points.Add(nextPos);
                    currentPos = nextPos;
                }
            }
        }

        predictionRenderer.positionCount = points.Count;
        predictionRenderer.SetPositions(points.ToArray());
    }

    public void Clear()
    {
        predictionRenderer.positionCount = 0;
    }
}
