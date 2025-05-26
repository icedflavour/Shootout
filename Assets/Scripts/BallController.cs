using UnityEngine;

public class BallController : MonoBehaviour
{
    public float moveDuration = 1.0f;

    private Rigidbody rb;
    private Vector3[] path;
    private bool isMoving = false;
    private float timer;
    private float finalSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    public void LaunchBall(Vector3[] curvePoints, float speedAfter)
    {
        path = curvePoints;
        timer = 0;
        isMoving = true;
        finalSpeed = speedAfter;
        rb.isKinematic = true;
    }

    void FixedUpdate()
    {
        if (!isMoving || path == null || path.Length < 2)
            return;

        timer += Time.fixedDeltaTime;
        float t = Mathf.Clamp01(timer / moveDuration);

        // EaseOut
        float easedT = 1f - Mathf.Pow(1f - t, 2);

        int index = Mathf.FloorToInt(easedT * (path.Length - 1));
        float localT = easedT * (path.Length - 1) - index;
        index = Mathf.Clamp(index, 0, path.Length - 2);

        Vector3 pos = Vector3.Lerp(path[index], path[index + 1], localT);
        transform.position = pos;

        if (t >= 1f)
        {
            isMoving = false;
            rb.isKinematic = false;

            Vector3 dir = (path[^1] - path[^2]).normalized;
            rb.velocity = dir * finalSpeed;
        }
    }
}
