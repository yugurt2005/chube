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
        if (cam.orthographicSize > 15.4) cam.orthographicSize = 15.3f;
        else if (cam.orthographicSize < 5) cam.orthographicSize = 5.1f;

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0) cam.orthographicSize += scroll * -3.5f;        
    }
}
