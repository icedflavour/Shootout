using UnityEngine;
using System.Collections.Generic;

public class Bezier : MonoBehaviour
{
<<<<<<< HEAD
    // Исходные точки свайпа (задайте в инспекторе или сформируйте самостоятельно)
    public List<Vector3> swipePoints;

    // Результат — 4 контрольные точки Безье
    private Vector3 P0, P1, P2, P3;

    [SerializeField] private int iterations = 1000;     // Количество итераций градиентного спуска
    [SerializeField] private float learningRate = 0.01f; // Скорость обучения (шаг)

=======
    // РСЃС…РѕРґРЅС‹Рµ С‚РѕС‡РєРё СЃРІР°Р№РїР° (Р·Р°РґР°Р№С‚Рµ РІ РёРЅСЃРїРµРєС‚РѕСЂРµ РёР»Рё СЃС„РѕСЂРјРёСЂСѓР№С‚Рµ СЃР°РјРѕСЃС‚РѕСЏС‚РµР»СЊРЅРѕ)
    public List<Vector3> swipePoints;

    // Р РµР·СѓР»СЊС‚Р°С‚ вЂ” 4 РєРѕРЅС‚СЂРѕР»СЊРЅС‹Рµ С‚РѕС‡РєРё Р‘РµР·СЊРµ
    private Vector3 P0, P1, P2, P3;

    [SerializeField] private int iterations = 1000;     // РљРѕР»РёС‡РµСЃС‚РІРѕ РёС‚РµСЂР°С†РёР№ РіСЂР°РґРёРµРЅС‚РЅРѕРіРѕ СЃРїСѓСЃРєР°
    [SerializeField] private float learningRate = 0.01f; // РЎРєРѕСЂРѕСЃС‚СЊ РѕР±СѓС‡РµРЅРёСЏ (С€Р°Рі)
    
>>>>>>> 7f2fdd2c7cf153d8b8b93cc3c4f4db142b645540
    public void ApproxBezier()
    {
        if (swipePoints == null || swipePoints.Count < 2)
        {
<<<<<<< HEAD
            Debug.LogError("Недостаточно точек для аппроксимации.");
            return;
        }

        // 1) Параметры t
=======
            Debug.LogError("РќРµРґРѕСЃС‚Р°С‚РѕС‡РЅРѕ С‚РѕС‡РµРє РґР»СЏ Р°РїРїСЂРѕРєСЃРёРјР°С†РёРё.");
            return;
        }

        // 1) РџР°СЂР°РјРµС‚СЂС‹ t
>>>>>>> 7f2fdd2c7cf153d8b8b93cc3c4f4db142b645540
        int n = swipePoints.Count;
        List<float> tList = new List<float>();
        for (int i = 0; i < n; i++)
        {
            tList.Add((float)i / (n - 1));
        }

<<<<<<< HEAD
        // 2) Задаем P0 и P3
        P0 = swipePoints[0];
        P3 = swipePoints[n - 1];

        // Инициализируем начальные P1 и P2 как некие точки на прямой (P0,P3).
        // Например, пусть P1 чуть смещена на 1/3, P2 на 2/3 отрезка [P0,P3]
        P1 = Vector3.Lerp(P0, P3, 1f / 3f);
        P2 = Vector3.Lerp(P0, P3, 2f / 3f);

        // 3) Итерируем градиентный спуск
        for (int iter = 0; iter < iterations; iter++)
        {
            // Вычислим градиенты для P1 и P2
            Vector3 gradP1 = Vector3.zero;
            Vector3 gradP2 = Vector3.zero;

=======
        // 2) Р—Р°РґР°РµРј P0 Рё P3
        P0 = swipePoints[0];
        P3 = swipePoints[n - 1];

        // РРЅРёС†РёР°Р»РёР·РёСЂСѓРµРј РЅР°С‡Р°Р»СЊРЅС‹Рµ P1 Рё P2 РєР°Рє РЅРµРєРёРµ С‚РѕС‡РєРё РЅР° РїСЂСЏРјРѕР№ (P0,P3).
        // РќР°РїСЂРёРјРµСЂ, РїСѓСЃС‚СЊ P1 С‡СѓС‚СЊ СЃРјРµС‰РµРЅР° РЅР° 1/3, P2 РЅР° 2/3 РѕС‚СЂРµР·РєР° [P0,P3]
        P1 = Vector3.Lerp(P0, P3, 1f/3f);
        P2 = Vector3.Lerp(P0, P3, 2f/3f);

        // 3) РС‚РµСЂРёСЂСѓРµРј РіСЂР°РґРёРµРЅС‚РЅС‹Р№ СЃРїСѓСЃРє
        for (int iter = 0; iter < iterations; iter++)
        {
            // Р’С‹С‡РёСЃР»РёРј РіСЂР°РґРёРµРЅС‚С‹ РґР»СЏ P1 Рё P2
            Vector3 gradP1 = Vector3.zero;
            Vector3 gradP2 = Vector3.zero;
            
>>>>>>> 7f2fdd2c7cf153d8b8b93cc3c4f4db142b645540
            for (int i = 0; i < n; i++)
            {
                float t = tList[i];
                Vector3 Q = swipePoints[i];

<<<<<<< HEAD
                // Текущее B(t)
                Vector3 B = BezierPoint(P0, P1, P2, P3, t);
                // Ошибка = B - Q
                Vector3 diff = B - Q;

                // Производные B(t) по P1 и P2 (частные)
=======
                // РўРµРєСѓС‰РµРµ B(t)
                Vector3 B = BezierPoint(P0, P1, P2, P3, t);
                // РћС€РёР±РєР° = B - Q
                Vector3 diff = B - Q;

                // РџСЂРѕРёР·РІРѕРґРЅС‹Рµ B(t) РїРѕ P1 Рё P2 (С‡Р°СЃС‚РЅС‹Рµ)
>>>>>>> 7f2fdd2c7cf153d8b8b93cc3c4f4db142b645540
                // dB/dP1 = 3(1 - t)^2 * t
                // dB/dP2 = 3(1 - t) * t^2
                float dB1 = 3 * Mathf.Pow((1 - t), 2) * t;
                float dB2 = 3 * (1 - t) * Mathf.Pow(t, 2);

<<<<<<< HEAD
                // Градиент (сумма d(Error)/dP1 или dP2)
                // d(Error)/dP1 = 2 * diff * dB/dP1 (по каждому i)
=======
                // Р“СЂР°РґРёРµРЅС‚ (СЃСѓРјРјР° d(Error)/dP1 РёР»Рё dP2)
                // d(Error)/dP1 = 2 * diff * dB/dP1 (РїРѕ РєР°Р¶РґРѕРјСѓ i)
>>>>>>> 7f2fdd2c7cf153d8b8b93cc3c4f4db142b645540
                gradP1 += 2f * diff * dB1;
                gradP2 += 2f * diff * dB2;
            }

<<<<<<< HEAD
            // Обновляем P1 и P2 (шагаем против градиента)
=======
            // РћР±РЅРѕРІР»СЏРµРј P1 Рё P2 (С€Р°РіР°РµРј РїСЂРѕС‚РёРІ РіСЂР°РґРёРµРЅС‚Р°)
>>>>>>> 7f2fdd2c7cf153d8b8b93cc3c4f4db142b645540
            P1 -= gradP1 * learningRate;
            P2 -= gradP2 * learningRate;
        }

<<<<<<< HEAD
        // По итогам итераций P0, P1, P2, P3 — ваши искомые контрольные точки
        Debug.Log($"P0 = {P0}, P1 = {P1}, P2 = {P2}, P3 = {P3}");
    }

    // Функция для вычисления точки на кривой Безье по t
=======
        // РџРѕ РёС‚РѕРіР°Рј РёС‚РµСЂР°С†РёР№ P0, P1, P2, P3 вЂ” РІР°С€Рё РёСЃРєРѕРјС‹Рµ РєРѕРЅС‚СЂРѕР»СЊРЅС‹Рµ С‚РѕС‡РєРё
        Debug.Log($"P0 = {P0}, P1 = {P1}, P2 = {P2}, P3 = {P3}");
    }

    // Р¤СѓРЅРєС†РёСЏ РґР»СЏ РІС‹С‡РёСЃР»РµРЅРёСЏ С‚РѕС‡РєРё РЅР° РєСЂРёРІРѕР№ Р‘РµР·СЊРµ РїРѕ t
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
