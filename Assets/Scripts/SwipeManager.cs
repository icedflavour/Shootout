using UnityEngine;
using System.Collections.Generic;

public class SwipeManager : MonoBehaviour
{
    public Transform ballTransform;
    public LineRenderer trajectoryRenderer;
    public LineRenderer swipeLineRenderer;
    public LineRenderer bezierPointRenderer;
    public Material pointMarkerMaterial;

    public BallController ballController;
    public TrajectoryPredictor trajectoryPredictor;

    [Header("Параметры удара")]
    [SerializeField] private float swipeDistanceMultiplier = 5f;
    [SerializeField] private float sliderPower = 1f;

    private List<Vector2> swipePoints = new List<Vector2>();
    private Camera cam;
    private bool isSwiping = false;
    private Vector3 initialBallPosition;

    void Start()
    {
        cam = Camera.main;
        initialBallPosition = ballTransform.position;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            swipePoints.Clear();
            isSwiping = true;

            // Сброс мяча
            ballTransform.position = initialBallPosition;
            Rigidbody rb = ballTransform.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.isKinematic = true;
            }

            trajectoryPredictor.Clear();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isSwiping = false;

            if (swipePoints.Count >= 2)
            {
                swipeLineRenderer.positionCount = 0;
                BuildAndDrawCurve();
            }
        }

        if (isSwiping)
        {
            swipePoints.Add(Input.mousePosition);

            // Визуализация свайпа
            swipeLineRenderer.positionCount = swipePoints.Count;
            for (int i = 0; i < swipePoints.Count; i++)
            {
                Vector3 worldPos = cam.ScreenToWorldPoint(new Vector3(swipePoints[i].x, swipePoints[i].y, 1f));
                swipeLineRenderer.SetPosition(i, worldPos);
            }
        }
    }

    public void SetSliderPower(float power)
    {
        sliderPower = Mathf.Clamp01(power);
    }
    void BuildAndDrawCurve()
    {
        Vector3 P0 = ballTransform.position;

        // 2D свайп
        Vector2 swipeDir2D = (swipePoints[^1] - swipePoints[0]).normalized;
        Vector3 swipeDir = cam.transform.TransformDirection(new Vector3(swipeDir2D.x, swipeDir2D.y, 1f).normalized);

        // Конечная точка дуги (зависит от силы удара)
        Vector3 P3 = P0 + swipeDir * swipeDistanceMultiplier * sliderPower;

        // Расчёт направления касательной по всей дуге
        Vector3 forwardDir = (P3 - P0).normalized;
        float controlLength = Vector3.Distance(P0, P3) * 0.25f;

        // Контрольные точки по направлению дуги
        Vector3 P1 = P0 + forwardDir * controlLength + Vector3.up * 2f;
        Vector3 P2 = P3 - forwardDir * controlLength + Vector3.up * 2f;

        // Построение кривой Безье
        int segments = 20;
        Vector3[] curvePoints = new Vector3[segments + 1];
        for (int i = 0; i <= segments; i++)
        {
            float t = i / (float)segments;
            curvePoints[i] = CalculateBezierPoint(t, P0, P1, P2, P3);
        }

        // Отрисовка кривой
        trajectoryRenderer.positionCount = curvePoints.Length;
        trajectoryRenderer.SetPositions(curvePoints);

        // Прогноз движения после дуги
        Vector3 finalDirection = (curvePoints[^1] - curvePoints[^2]).normalized;
        float predictedSpeed = sliderPower * 10f;
        trajectoryPredictor.Predict(curvePoints[^1], finalDirection, predictedSpeed);

        // Запуск мяча
        ballController.LaunchBall(curvePoints, predictedSpeed);
    }


    Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        Vector3 point = uuu * p0;
        point += 3 * uu * t * p1;
        point += 3 * u * tt * p2;
        point += ttt * p3;

        return point;
    }


    void VisualizeBezierPoints(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        // Отрисуем линии между точками для наглядности
        bezierPointRenderer.positionCount = 4;
        bezierPointRenderer.SetPosition(0, p0);
        bezierPointRenderer.SetPosition(1, p1);
        bezierPointRenderer.SetPosition(2, p2);
        bezierPointRenderer.SetPosition(3, p3);

        // Нарисуем точки — создадим временные сферы
        DrawMarker(p0, Color.green);
        DrawMarker(p1, Color.yellow);
        DrawMarker(p2, Color.yellow);
        DrawMarker(p3, Color.red);
    }

    void DrawMarker(Vector3 position, Color color)
    {
        GameObject marker = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        marker.transform.position = position;
        marker.transform.localScale = Vector3.one * 0.2f;
        marker.GetComponent<Renderer>().material = pointMarkerMaterial;
        marker.GetComponent<Renderer>().material.color = color;
        Destroy(marker, 2f); // автоматическое удаление через 2 секунды
    }

}
