using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTracker : MonoBehaviour
{
    private Camera camera;

    private void Start()
    {
        camera = Camera.main;
    }

    private void Update()
    {
        // получение координат мышки
        Vector3 screenMousePosition = Input.mousePosition;
        Vector3 worldMousePosition = camera.ScreenToWorldPoint(screenMousePosition);

        transform.LookAt(worldMousePosition);

        Debug.Log(worldMousePosition);
    }
}
