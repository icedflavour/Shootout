using UnityEngine;
using System.Collections.Generic;

public class KickerController : MonoBehaviour
{
    [SerializeField] private int KickPower;
    private List<Vector3> swipeCurve;
    private Vector3[] flyDirection;
    private GameObject ball;

    private int iterator = 0; // Deletable

    private void Start()
    {
        swipeCurve = new List<Vector3>();
    }

    private void Update()
    {
        var userMousePosition = Input.mousePosition;

        if (Input.GetMouseButtonDown(0))
        {
            swipeCurve.Add(new Vector3(userMousePosition.x, userMousePosition.y));
            
            Debug.Log(swipeCurve[iterator]);  // Deletable
            Debug.Log("Length: " + swipeCurve.Count); // Deletable
            iterator++; // Deletable
        } 
    }

}
