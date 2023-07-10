using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;

public class Lines : MonoBehaviour
{
    public LineRenderer circle;
    public GameObject solar;
    void Start()
    {
        DrawCircle(100, 250);
    }

    void DrawCircle(int steps, float radius) {
        circle.positionCount = steps;
        for (int i = 0; i < steps; i++) {
            float circumferenceProgress = (float)i / steps;
            float currentRadian = circumferenceProgress * 2 * Mathf.PI;

            float xScaled = Mathf.Cos(currentRadian);
            float zScaled = Mathf.Sin(currentRadian);

            float x = solar.transform.position.x + xScaled * radius;
            float z = solar.transform.position.z + zScaled * radius;

            Vector3 currentPos = new Vector3(1229/3.6f+x, solar.transform.position.y, z);
            circle.SetPosition(i, currentPos);
        }
    }
}
