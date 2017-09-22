using UnityEngine;

[RequireComponent(typeof(Camera))]

public class CameraController : MonoBehaviour {
    [Range(4.0f, 20.0f)] public float RotateAmount = 8f;
    public Transform rotationPoint;
    public float zoomSpeed;
    public Vector2 zoomLimit;
    public GameObject LeftCameraView;
    public GameObject RightCameraView;
    private Vector3 startPos;
    private Quaternion startRot;


    void Start() {
        startPos = GetComponent<Transform>().position;
        startRot = GetComponent<Transform>().rotation;
    }

    // when rendering camera always use late update.
    void LateUpdate() {
        if (Input.GetMouseButton(2)) {
            RotateCamera();
        }
        else if (Input.GetKeyDown(KeyCode.W)) {
            GetDefaultCameraView();
        }
        if ((Input.GetKeyDown(KeyCode.Q)))
        {
            ChangeCameraPosition(LeftCameraView.transform.position, LeftCameraView.transform.rotation);
        }
        if ((Input.GetKeyDown(KeyCode.E)))
        {
            ChangeCameraPosition(RightCameraView.transform.position, RightCameraView.transform.rotation);
        }
        ZoomCamera();
    }


    public void RotateCamera() {
        Vector3 target = rotationPoint.transform.position;
        float x_rotate = Input.GetAxis("Mouse X") * RotateAmount;
        OrbitCamera(target, x_rotate, 0.0f);
    }


    //returns the camera to the initial view.
    public void GetDefaultCameraView() {
        gameObject.transform.position = startPos;
        gameObject.transform.rotation = startRot;
    }

    public void OrbitCamera(Vector3 target, float y_rotate, float x_rotate) {
        transform.RotateAround(target, Vector3.up, y_rotate);
        transform.LookAt(target);
    }

    public void ZoomCamera()
    {
        Camera.main.fieldOfView -= Input.mouseScrollDelta.y * zoomSpeed;
        Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, zoomLimit.x, zoomLimit.y);
    }

    public void ChangeCameraPosition(Vector3 position , Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
    }

}