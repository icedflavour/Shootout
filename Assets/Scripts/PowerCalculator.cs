using UnityEngine;
using UnityEngine.UI;

public class PowerCalculator : MonoBehaviour
{
    public Slider slider;
    public float speed = 1f; // Current speed
    private float t = 0f; // Internal time counter
    private bool isMoving = true; // Flag to check if the slider is moving

    private void FixedUpdate()
    {
        Debug.Log(speed);
        if (slider == null && isMoving == true) return;

        // Move the sine wave smoothly using internal time
        t += Time.fixedDeltaTime * speed;
        float value = ((Mathf.Sin(t) + 1) / 2f) * 100f;
        slider.value = value;

        // Adjust speed based on current slider value (not Time.time)
        if (value < 65f)
        {
            speed = 1.5f;
        }
        else if (value >= 65f && value <= 90f)
        {
            speed = 2.5f;
        }
        else if (value > 90f)
        {
            speed = 4f;
        }
        
    }
    void Update()
    {
         if (Input.GetMouseButtonDown(0))
         {
            isMoving = false;
            speed = 0f; // Stop the sine wave movement
            
         }
}
}
