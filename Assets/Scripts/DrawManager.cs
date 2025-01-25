using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawManager : MonoBehaviour
{
    private Camera _cam;
    [SerializeField] private GameObject _canvas;
    [SerializeField] private Line _linePrefab;
    [SerializeField] private Bezier _bezier;
<<<<<<< HEAD
    [SerializeField] private Transform ball; // Ссылка на объект мяча
=======
    [SerializeField] private Transform ball; // РЎСЃС‹Р»РєР° РЅР° РѕР±СЉРµРєС‚ РјСЏС‡Р°
>>>>>>> 7f2fdd2c7cf153d8b8b93cc3c4f4db142b645540

    public const float RESOLUTION = .1f;

    private Line _currentLine;

    void Start()
    {
        _cam = Camera.main;
        Debug.Log(_canvas.GetComponent<Canvas>().pixelRect);
<<<<<<< HEAD
        _bezier = GetComponent<Bezier>();
=======
>>>>>>> 7f2fdd2c7cf153d8b8b93cc3c4f4db142b645540
    }

    void Update()
    {
        Vector2 mousePos = Input.mousePosition;

        if (Input.GetMouseButtonDown(0))
        {
            if (_currentLine != null)
            {
                Destroy(_currentLine.gameObject);
            }

            _currentLine = Instantiate(_linePrefab, _canvas.transform);
        }

        if (Input.GetMouseButton(0))
        {
            _currentLine.SetPosition(mousePos, _canvas.GetComponent<Canvas>());
        }

        if (Input.GetMouseButtonUp(0))
        {
<<<<<<< HEAD
            // Получаем точки свайпа
            List<Vector2> swipePoints2D = _currentLine.GetComponent<Line>().GetPoints();
            // Преобразуем их в 3D
            List<Vector3> swipePoints3D = ConvertTo3DPoints(swipePoints2D, ball.position);
            // Передаем в скрипт Bezier
            _bezier.swipePoints = swipePoints3D;
            _bezier.ApproxBezier();
        }
    }

    // Преобразование списка 2D точек в 3D
    private List<Vector3> ConvertTo3DPoints(List<Vector2> points2D, Vector3 initialPosition)
    {
        List<Vector3> points3D = new List<Vector3>();
        float zOffset = 0f; // Начальное смещение по оси Z

        for (int i = 0; i < points2D.Count; i++)
        {
            Vector2 point2D = points2D[i];

            // Преобразуем координаты мыши в мировые координаты
            Vector3 worldPoint = _cam.ScreenToWorldPoint(new Vector3(point2D.x, point2D.y, _cam.nearClipPlane));
            // Добавляем смещение Z пропорционально индексу точки
            zOffset += 0.1f; // Задаем шаг по Z (регулируется)
            Vector3 point3D = new Vector3(worldPoint.x, worldPoint.y, initialPosition.z + zOffset);

            points3D.Add(point3D);
        }

        return points3D;
=======
            // РџРѕР»СѓС‡Р°РµРј С‚РѕС‡РєРё СЃРІР°Р№РїР°
            List<Vector2> swipePoints2D = _currentLine.GetComponent<Line>().GetPoints();
            // РџСЂРµРѕР±СЂР°Р·СѓРµРј РёС… РІ 3D
            List<Vector3> swipePoints3D = ConvertTo3DPoints(swipePoints2D, ball.position);
            // РџРµСЂРµРґР°РµРј РІ СЃРєСЂРёРїС‚ Bezier
            _bezier.swipePoints = swipePoints3D;
            _bezier.ApproxBezier();
        }
>>>>>>> 7f2fdd2c7cf153d8b8b93cc3c4f4db142b645540
    }

    // РџСЂРµРѕР±СЂР°Р·РѕРІР°РЅРёРµ СЃРїРёСЃРєР° 2D С‚РѕС‡РµРє РІ 3D
    private List<Vector3> ConvertTo3DPoints(List<Vector2> points2D, Vector3 initialPosition)
    {
        List<Vector3> points3D = new List<Vector3>();
        float zOffset = 0f; // РќР°С‡Р°Р»СЊРЅРѕРµ СЃРјРµС‰РµРЅРёРµ РїРѕ РѕСЃРё Z

        for (int i = 0; i < points2D.Count; i++)
        {
            Vector2 point2D = points2D[i];

            // РџСЂРµРѕР±СЂР°Р·СѓРµРј РєРѕРѕСЂРґРёРЅР°С‚С‹ РјС‹С€Рё РІ РјРёСЂРѕРІС‹Рµ РєРѕРѕСЂРґРёРЅР°С‚С‹
            Vector3 worldPoint = _cam.ScreenToWorldPoint(new Vector3(point2D.x, point2D.y, _cam.nearClipPlane));
            // Р”РѕР±Р°РІР»СЏРµРј СЃРјРµС‰РµРЅРёРµ Z РїСЂРѕРїРѕСЂС†РёРѕРЅР°Р»СЊРЅРѕ РёРЅРґРµРєСЃСѓ С‚РѕС‡РєРё
            zOffset += 0.1f; // Р—Р°РґР°РµРј С€Р°Рі РїРѕ Z (СЂРµРіСѓР»РёСЂСѓРµС‚СЃСЏ)
            Vector3 point3D = new Vector3(worldPoint.x, worldPoint.y, initialPosition.z + zOffset);

            points3D.Add(point3D);
        }

        return points3D;
    }
}
