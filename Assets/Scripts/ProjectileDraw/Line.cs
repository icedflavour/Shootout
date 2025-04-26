using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField] private LineRenderer _renderer;

    private readonly List<Vector2> _points = new List<Vector2>();

    public void SetPosition(Vector2 pos, Canvas canv)
    {
        if (!CanAppend(pos)) return;

        Vector2 anchoredPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canv.transform as RectTransform, pos, canv.worldCamera, out anchoredPos);

        _points.Add(pos);
        _renderer.positionCount++;
        _renderer.SetPosition(_renderer.positionCount - 1, anchoredPos);
    }

    public List<Vector2> GetPoints()
    {
        return _points;
    }

    private bool CanAppend(Vector2 pos)
    {
        if (_points.Count == 0) return true;
        return Vector2.Distance(_points[_points.Count - 1], pos) > DrawManager.RESOLUTION;
    }
}
