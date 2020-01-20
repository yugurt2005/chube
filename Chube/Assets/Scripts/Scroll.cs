using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        if (cam.orthographicSize > 15) cam.orthographicSize = 14.9f;
        else if (cam.orthographicSize < 2) cam.orthographicSize = 2.1f;

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0) cam.orthographicSize += scroll * -3.5f;        
    }
}
