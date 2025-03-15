using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField] private LineRenderer _renderer;

    private readonly List<Vector2> _points = new List<Vector2>();

    public void SetPosition(Vector2 pos, Canvas canv)
    {
        if (!CanAppend(pos, canv)) return;

        pos = new Vector2(pos.x - canv.pixelRect.width / 2, pos.y - canv.pixelRect.height / 2);
        _points.Add(pos);

        _renderer.positionCount++;
        _renderer.SetPosition(_renderer.positionCount - 1, pos);
    }

    public List<Vector2> GetPoints()
    {
        return _points;
    }

    private bool CanAppend(Vector2 pos, Canvas canv)
    {
        if (_renderer.positionCount == 0) return true;

        return Vector2.Distance(_renderer.GetPosition(_renderer.positionCount - 1), pos) > DrawManager.RESOLUTION;
    }
}
