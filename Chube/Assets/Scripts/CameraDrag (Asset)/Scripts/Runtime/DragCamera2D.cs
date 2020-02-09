using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// NOTE: This is an asset that I edited
public class DragCamera2D : MonoBehaviour
{
    public Camera cam;

    [Header("Camera Movement")]

    [Tooltip("Allow the Camera to be dragged.")]
    public bool dragEnabled = true;
    [Range(-5, 5)]
    [Tooltip("Speed the camera moves when dragged.")]
    public float dragSpeed = -0.06f;

    [Header("Zoom")]
    [Tooltip("Enable or disable zooming")]
    public bool zoomEnabled = true;
    [Tooltip("Scale drag movement with zoom level")]
    public bool linkedZoomDrag = true;
    [Tooltip("Maximum Zoom Level")]
    public float maxZoom = 10;
    [Tooltip("Minimum Zoom Level")]
    [Range(0.01f, 10)]
    public float minZoom = 0.5f;
    [Tooltip("The Speed the zoom changes")]
    [Range(0.1f, 10f)]
    public float zoomStepSize = 0.5f;
    [Tooltip("Enable Zooming to mouse pointer (Pro Only)")]
    public bool zoomToMouse = false;

    [Header("Camera Bounds")]
    public bool clampCamera = true;
    public CameraBounds bounds; 

    // private vars
    Vector3 bl;
    Vector3 tr;
    private Vector2 touchOrigin = -Vector2.one;

    void Start() {
        if (cam == null) {
            cam = Camera.main;
        }
    }

    void Update() {
        if (dragEnabled) {
            panControl();
        }
        if (zoomEnabled) {
            zoomControl();
        }
        if (clampCamera) {
            cameraClamp();
        }
       
    }

    public void addCameraBounds() {
        if (bounds == null) {
            GameObject go = new GameObject("CameraBounds");
            CameraBounds cb = go.AddComponent<CameraBounds>();
            cb.guiColour = new Color(0,0,1f,0.1f);
            cb.pointa = new Vector3(20,20,0);
            this.bounds = cb;
            EditorUtility.SetDirty(this);
        }
    }

    //click and drag
    public void panControl() {

       // if mouse is down
        if (Input.GetMouseButton(1)) {
            float x = Input.GetAxis("Mouse X") * dragSpeed;
            float y = Input.GetAxis("Mouse Y") * dragSpeed;

            if (linkedZoomDrag) {
                x *= Camera.main.orthographicSize;
                y *= Camera.main.orthographicSize;
            }

            transform.Translate(x, y, 0);
        }        
    }

    private void clampZoom() {
        Camera.main.orthographicSize =  Mathf.Clamp(Camera.main.orthographicSize, minZoom, maxZoom);
        Mathf.Max(cam.orthographicSize, 0.1f);
    }

    void ZoomOrthoCamera(Vector3 zoomTowards, float amount) {
        // Calculate how much we will have to move towards the zoomTowards position
        float multiplier = (1.0f / Camera.main.orthographicSize * amount);
        // Move camera
        transform.position += (zoomTowards - transform.position) * multiplier;
        // Zoom camera
        Camera.main.orthographicSize -= amount;
        // Limit zoom
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, minZoom, maxZoom);
    }

    // managae zooming
    public void zoomControl() {
        if (Input.GetAxis("Mouse ScrollWheel") > 0) // forward
        {
            Camera.main.orthographicSize = Camera.main.orthographicSize - zoomStepSize;
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0) // back            
        {
            Camera.main.orthographicSize = Camera.main.orthographicSize + zoomStepSize;
        }
        clampZoom();
    }

    // Clamp Camera to bounds
    private void cameraClamp() {
        tr = cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, cam.pixelHeight, -transform.position.z));
        bl = cam.ScreenToWorldPoint(new Vector3(0, 0, -transform.position.z));

        if(bounds == null) {
            Debug.Log("Clamp Camera Enabled but no Bounds has been set.");
            return;
        }

        float boundsMaxX = bounds.pointa.x;
        float boundsMinX = bounds.transform.position.x;
        float boundsMaxY = bounds.pointa.y;
        float boundsMinY = bounds.transform.position.y;

        if (tr.x > boundsMaxX) {
            transform.position = new Vector3(transform.position.x - (tr.x - boundsMaxX), transform.position.y, transform.position.z);
        }

        if (tr.y > boundsMaxY) {
            transform.position = new Vector3(transform.position.x, transform.position.y - (tr.y - boundsMaxY), transform.position.z);
        }

        if (bl.x < boundsMinX) {
            transform.position = new Vector3(transform.position.x + (boundsMinX - bl.x), transform.position.y, transform.position.z);
        }

        if (bl.y < boundsMinY) {
            transform.position = new Vector3(transform.position.x, transform.position.y + (boundsMinY - bl.y), transform.position.z);
        }
    }
}
