using UnityEngine;
using UnityEngine.UI;

public class Slider : MonoBehaviour
{
    public Slider slider;
    private float slider.value = 0f;
    public float speed = 1f;  // Скорость движения

    private void Update()
    {
        if (slider != null)
        {
            // Используем синус для плавного движения туда-обратно
            slider.value = (Mathf.Sin(Time.time * speed) + 1) / 2;
        }
    }
}

