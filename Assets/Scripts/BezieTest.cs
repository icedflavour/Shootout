using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class BezieTest : MonoBehaviour
{
    public Transform P0;
    public Transform P2;
    public Transform P3;
    public Transform P1;

    [Range(0,1)]
    public float t;
    
    void Update()
    {
        transform.position = Bezier.GetPoint(P0.position, P1.position, P2.position, P3.position, t);
    }

    private void OnDrawGizmoz()
    {
        int _segmentNumber = 20;
        Vector3 previousPoint = P0.position;

        for (int i = 0; i < _segmentNumber + 1; i++)
        {
            float parameter = (float)i / _segmentNumber;
            Vector3 point = Bezier.GetPoint(P0.position, P1.position, P2.position, P3.position, parameter);
            Gizmos.DrawLine(previousPoint, point);
            previousPoint = point;
        }
    }
}
