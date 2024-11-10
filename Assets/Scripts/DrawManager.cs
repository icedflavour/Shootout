using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawManager : MonoBehaviour
{
    private Camera _cam;
    [SerializeField] private Transform _canvas;
    [SerializeField] private Line _linePrefab;

    public const float RESOLUTION = .1f;

    private Line _currentLine;
    void Start()
    {
        _cam = Camera.main;
    }


    void Update()
    {
        Vector2 mousePos = Input.mousePosition;

        if (Input.GetMouseButtonDown(0)) _currentLine = Instantiate(_linePrefab, Vector3.zero, Quaternion.identity, _canvas);

        if (Input.GetMouseButton(0)) _currentLine.SetPosition(mousePos);
    }
}