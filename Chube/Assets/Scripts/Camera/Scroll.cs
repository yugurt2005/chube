using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    // TODO: 
    // - drag to move camera around
    // - double click on something to focus on it

    Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, 3f, 15.4f);

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0) cam.orthographicSize += scroll * -3.5f;        
    }
}
