using UnityEngine;
using System.Collections.Generic;

public class Bezier : MonoBehaviour
{
<<<<<<< HEAD
    // �������� ����� ������ (������� � ���������� ��� ����������� ��������������)
    public List<Vector3> swipePoints;

    // ��������� � 4 ����������� ����� �����
    private Vector3 P0, P1, P2, P3;

    [SerializeField] private int iterations = 1000;     // ���������� �������� ������������ ������
    [SerializeField] private float learningRate = 0.01f; // �������� �������� (���)

=======
    // Исходные точки свайпа (задайте в инспекторе или сформируйте самостоятельно)
    public List<Vector3> swipePoints;

    // Результат — 4 контрольные точки Безье
    private Vector3 P0, P1, P2, P3;

    [SerializeField] private int iterations = 1000;     // Количество итераций градиентного спуска
    [SerializeField] private float learningRate = 0.01f; // Скорость обучения (шаг)
    
>>>>>>> 7f2fdd2c7cf153d8b8b93cc3c4f4db142b645540
    public void ApproxBezier()
    {
        if (swipePoints == null || swipePoints.Count < 2)
        {
<<<<<<< HEAD
            Debug.LogError("������������ ����� ��� �������������.");
            return;
        }

        // 1) ��������� t
=======
            Debug.LogError("Недостаточно точек для аппроксимации.");
            return;
        }

        // 1) Параметры t
>>>>>>> 7f2fdd2c7cf153d8b8b93cc3c4f4db142b645540
        int n = swipePoints.Count;
        List<float> tList = new List<float>();
        for (int i = 0; i < n; i++)
        {
            tList.Add((float)i / (n - 1));
        }

<<<<<<< HEAD
        // 2) ������ P0 � P3
        P0 = swipePoints[0];
        P3 = swipePoints[n - 1];

        // �������������� ��������� P1 � P2 ��� ����� ����� �� ������ (P0,P3).
        // ��������, ����� P1 ���� ������� �� 1/3, P2 �� 2/3 ������� [P0,P3]
        P1 = Vector3.Lerp(P0, P3, 1f / 3f);
        P2 = Vector3.Lerp(P0, P3, 2f / 3f);

        // 3) ��������� ����������� �����
        for (int iter = 0; iter < iterations; iter++)
        {
            // �������� ��������� ��� P1 � P2
            Vector3 gradP1 = Vector3.zero;
            Vector3 gradP2 = Vector3.zero;

=======
        // 2) Задаем P0 и P3
        P0 = swipePoints[0];
        P3 = swipePoints[n - 1];

        // Инициализируем начальные P1 и P2 как некие точки на прямой (P0,P3).
        // Например, пусть P1 чуть смещена на 1/3, P2 на 2/3 отрезка [P0,P3]
        P1 = Vector3.Lerp(P0, P3, 1f/3f);
        P2 = Vector3.Lerp(P0, P3, 2f/3f);

        // 3) Итерируем градиентный спуск
        for (int iter = 0; iter < iterations; iter++)
        {
            // Вычислим градиенты для P1 и P2
            Vector3 gradP1 = Vector3.zero;
            Vector3 gradP2 = Vector3.zero;
            
>>>>>>> 7f2fdd2c7cf153d8b8b93cc3c4f4db142b645540
            for (int i = 0; i < n; i++)
            {
                float t = tList[i];
                Vector3 Q = swipePoints[i];

<<<<<<< HEAD
                // ������� B(t)
                Vector3 B = BezierPoint(P0, P1, P2, P3, t);
                // ������ = B - Q
                Vector3 diff = B - Q;

                // ����������� B(t) �� P1 � P2 (�������)
=======
                // Текущее B(t)
                Vector3 B = BezierPoint(P0, P1, P2, P3, t);
                // Ошибка = B - Q
                Vector3 diff = B - Q;

                // Производные B(t) по P1 и P2 (частные)
>>>>>>> 7f2fdd2c7cf153d8b8b93cc3c4f4db142b645540
                // dB/dP1 = 3(1 - t)^2 * t
                // dB/dP2 = 3(1 - t) * t^2
                float dB1 = 3 * Mathf.Pow((1 - t), 2) * t;
                float dB2 = 3 * (1 - t) * Mathf.Pow(t, 2);

<<<<<<< HEAD
                // �������� (����� d(Error)/dP1 ��� dP2)
                // d(Error)/dP1 = 2 * diff * dB/dP1 (�� ������� i)
=======
                // Градиент (сумма d(Error)/dP1 или dP2)
                // d(Error)/dP1 = 2 * diff * dB/dP1 (по каждому i)
>>>>>>> 7f2fdd2c7cf153d8b8b93cc3c4f4db142b645540
                gradP1 += 2f * diff * dB1;
                gradP2 += 2f * diff * dB2;
            }

<<<<<<< HEAD
            // ��������� P1 � P2 (������ ������ ���������)
=======
            // Обновляем P1 и P2 (шагаем против градиента)
>>>>>>> 7f2fdd2c7cf153d8b8b93cc3c4f4db142b645540
            P1 -= gradP1 * learningRate;
            P2 -= gradP2 * learningRate;
        }

<<<<<<< HEAD
        // �� ������ �������� P0, P1, P2, P3 � ���� ������� ����������� �����
        Debug.Log($"P0 = {P0}, P1 = {P1}, P2 = {P2}, P3 = {P3}");
    }

    // ������� ��� ���������� ����� �� ������ ����� �� t
=======
        // По итогам итераций P0, P1, P2, P3 — ваши искомые контрольные точки
        Debug.Log($"P0 = {P0}, P1 = {P1}, P2 = {P2}, P3 = {P3}");
    }

    // Функция для вычисления точки на кривой Безье по t
>>>>>>> 7f2fdd2c7cf153d8b8b93cc3c4f4db142b645540
    private Vector3 BezierPoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        float u = 1f - t;
        float u2 = u * u;
        float t2 = t * t;

        return (u2 * u) * p0 +
               (3f * u2 * t) * p1 +
               (3f * u * t2) * p2 +
               (t2 * t) * p3;
    }
<<<<<<< HEAD

    
}

=======
}
>>>>>>> 7f2fdd2c7cf153d8b8b93cc3c4f4db142b645540
