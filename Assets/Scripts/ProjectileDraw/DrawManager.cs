using UnityEngine;
using System.Collections.Generic;

public class DrawManager : MonoBehaviour
{
    public GameObject linePrefab;
    public Canvas canvas;
    public Camera cam;
    public ArcMovement ballMovement;
    public Transform ballTransform;

    private Line currentLine;

    public const float RESOLUTION = 5f;

    void Update()
{
    Vector2 mousePos = Input.mousePosition;

    if (Input.GetMouseButtonDown(0))
    {
        // 👉 Уничтожаем предыдущую линию
        if (currentLine != null)
        {
            Destroy(currentLine.gameObject);
        }

        // 👉 Сбрасываем мяч на начальную позицию
        ballMovement.ResetPosition();

        currentLine = Instantiate(linePrefab, canvas.transform).GetComponent<Line>();
    }

    if (Input.GetMouseButton(0))
    {
        if (currentLine != null)
            currentLine.SetPosition(mousePos, canvas);
    }

    if (Input.GetMouseButtonUp(0))
    {
        if (currentLine == null) return;

        List<Vector2> swipePoints2D = currentLine.GetPoints();
        if (swipePoints2D.Count < 2) return;

        List<Vector3> swipePoints3D = ConvertTo3DPoints(swipePoints2D, ballTransform.position);

        SetupBezierAndMove(swipePoints3D);

        Destroy(currentLine.gameObject);
        currentLine = null;
    }
}

    private void SetupBezierAndMove(List<Vector3> points)
{
    // point0 - всегда стартует из позиции мяча
    ballMovement.point0.position = ballTransform.position;

    // point3 - конец свайпа
    ballMovement.point3.position = points[points.Count - 1];

    Vector3 dir = ballMovement.point3.position - ballMovement.point0.position;
    Vector3 perpendicular = Vector3.Cross(dir, Vector3.up).normalized;

    float distance = dir.magnitude / 3f;

    // Для более гладкой дуги: берем первую точку свайпа как базу для первой контрольной точки
    if (points.Count >= 2)
        ballMovement.point1.position = Vector3.Lerp(ballMovement.point0.position, points[1], 0.5f) + perpendicular * distance * 0.5f;
    else
        ballMovement.point1.position = ballMovement.point0.position + dir / 3f + perpendicular * distance * 0.5f;

    // Вторую контрольную точку можно подстроить между серединой и концом свайпа
    if (points.Count >= 3)
        ballMovement.point2.position = Vector3.Lerp(points[points.Count / 2], ballMovement.point3.position, 0.5f) + perpendicular * distance * 0.5f;
    else
        ballMovement.point2.position = ballMovement.point0.position + dir * 2f / 3f + perpendicular * distance * 0.5f;

    ballMovement.StartMovement();
}


    private List<Vector3> ConvertTo3DPoints(List<Vector2> points2D, Vector3 initialPosition)
    {
        List<Vector3> points3D = new List<Vector3>();
        float zOffset = 0f;

        for (int i = 0; i < points2D.Count; i++)
        {
            Vector2 point2D = points2D[i];
            Vector3 worldPoint = cam.ScreenToWorldPoint(new Vector3(point2D.x, point2D.y, cam.nearClipPlane + 5f));

            zOffset += 0.05f;
            Vector3 point3D = new Vector3(worldPoint.x, worldPoint.y, initialPosition.z + zOffset);

            points3D.Add(point3D);
        }

        return points3D;
    }
}
