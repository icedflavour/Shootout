using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody), typeof(LineRenderer))]
public class ArcMovement : MonoBehaviour
{
    [Header("Bezier Control Points")]
    public Transform point0;
    public Transform point1;
    public Transform point2;
    public Transform point3;

    [Header("Movement Settings")]
    public float duration = 1.0f;
    public int trajectoryResolution = 30;

    private Rigidbody _rb;
    private LineRenderer _lineRenderer;

    private Vector3 _initialPosition; // для сброса позиции мяча

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.isKinematic = true;

        _lineRenderer = GetComponent<LineRenderer>();
        SetupLineRenderer();

        _initialPosition = transform.position; // запоминаем начальную позицию
    }

    void SetupLineRenderer()
    {
        _lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        _lineRenderer.startColor = Color.white;
        _lineRenderer.endColor = Color.white;
        _lineRenderer.widthMultiplier = 0.05f;
        _lineRenderer.positionCount = 0; // начально пустая линия
    }

    public void StartMovement()
    {
        DrawTrajectory();
        StopAllCoroutines();
        StartCoroutine(MoveAlongBezier());
    }

    private IEnumerator MoveAlongBezier()
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            transform.position = Bezier.GetPoint(point0.position, point1.position, point2.position, point3.position, t);
            yield return null;
        }

        EnablePhysics();
        ClearTrajectory();
    }

    private void EnablePhysics()
    {
        _rb.isKinematic = false;
        Vector3 forceDir = (point3.position - point2.position).normalized;
        _rb.AddForce(forceDir * 10f, ForceMode.Impulse);
    }

    void DrawTrajectory()
    {
        _lineRenderer.positionCount = trajectoryResolution + 1; // обязательно перед отрисовкой!
        for (int i = 0; i <= trajectoryResolution; i++)
        {
            float t = (float)i / trajectoryResolution;
            Vector3 point = Bezier.GetPoint(point0.position, point1.position, point2.position, point3.position, t);
            _lineRenderer.SetPosition(i, point);
        }
    }

    void ClearTrajectory()
    {
        _lineRenderer.positionCount = 0;
    }

    public void ResetPosition()
    {
        StopAllCoroutines();
        ClearTrajectory();
        _rb.isKinematic = true;
        transform.position = _initialPosition;
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
    }
}
