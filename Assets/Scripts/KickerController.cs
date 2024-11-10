using UnityEngine;
using System.Collections.Generic;

public class KickerController : MonoBehaviour
{
    [SerializeField] private int KickPower;
    private List<Vector3> swipeCurve;
    private Vector3[] flyDirection;
    private GameObject ball;

    private void Start()
    {
        swipeCurve = new List<Vector3>();
    }

    private void Update()
    {

    }

}
