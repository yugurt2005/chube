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
        if (cam.orthographicSize > 15.4) cam.orthographicSize = 15.39f;
        else if (cam.orthographicSize < 3) cam.orthographicSize = 3.01f;

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0) cam.orthographicSize += scroll * -3.5f;        
    }
}
