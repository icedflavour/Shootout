using UnityEngine;

public class ArcMovement : MonoBehaviour
{
    [Header("Bezier Control Points")]
    public Transform point0;
    public Transform point1;
    public Transform point2;
    public Transform point3;

    [Range(0f, 1f)]
    public float t = 0f;

    void Update()
    {
        if (point0 == null || point1 == null || point2 == null || point3 == null)
        {
            Debug.LogWarning("One or more control points are not set!");
            return;
        }

        // Двигаем объект по кривой Безье в зависимости от параметра t
        transform.position = Bezier.GetPoint(
            point0.position,
            point1.position,
            point2.position,
            point3.position,
            t
        );
    }
}
