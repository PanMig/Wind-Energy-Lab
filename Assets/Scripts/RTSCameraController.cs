using UnityEngine;

[RequireComponent(typeof(Camera))]

public class RTSCameraController : MonoBehaviour {

    public float ScreenEdgeBorderThickness = 5.0f;
    [Space]
    [Header("Speeds")]
    public float panSpeed;
    public float minPanSpeed;
    public float maxPanSpeed;
    public float secToMaxSpeed; //seconds taken to reach max speed;
    public float rotateSpeed;
    public float zoomSpeed;
 
    [Space]
    [Header("Movement Limits")]
    public Vector2 heightLimit;
    public Vector2 lenghtLimit;
    public Vector2 widthLimit;
    public Vector2 zoomLimit;

    private Vector3 initialPos;
    private Vector3 panMovement;
    private Vector3 pos;
    private Quaternion rot;
    private bool rotationActive = false;
    private Vector3 lastMousePosition;
    private Quaternion initialRot;
    private float panIncrease = 0.0f;



    // Use this for initialization
    void Start () {
        initialPos = transform.position;
        initialRot = transform.rotation;
	}
	
	
	void Update () {

        #region Movement
        if (!rotationActive)
        {
            panMovement = Vector3.zero;

            if (Input.GetKey(KeyCode.W) || Input.mousePosition.y >= Screen.height - ScreenEdgeBorderThickness)
            {
                panMovement += Vector3.forward * panSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.S) || Input.mousePosition.y <= ScreenEdgeBorderThickness)
            {
                panMovement -= Vector3.forward * panSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.A) || Input.mousePosition.x <= ScreenEdgeBorderThickness)
            {
                panMovement += Vector3.left * panSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - ScreenEdgeBorderThickness)
            {
                panMovement += Vector3.right * panSpeed * Time.deltaTime;
                //pos.x += panSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.Q))
            {
                panMovement += Vector3.up * panSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.E))
            {
                panMovement += Vector3.down * panSpeed * Time.deltaTime;
            }

        }
            transform.Translate(panMovement, Space.World);

        //increase pan speed
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) 
            || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)
            || Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.Q)
            || Input.mousePosition.y >= Screen.height - ScreenEdgeBorderThickness
            || Input.mousePosition.y <= ScreenEdgeBorderThickness
            || Input.mousePosition.x <= ScreenEdgeBorderThickness
            || Input.mousePosition.x >= Screen.width - ScreenEdgeBorderThickness)
        {
            panIncrease += Time.deltaTime / secToMaxSpeed;
            panSpeed = Mathf.Lerp(minPanSpeed, maxPanSpeed, panIncrease);
        }
        else
        {
            panIncrease = 0;
            panSpeed = minPanSpeed;
        }

        #endregion

        #region Zoom

        Camera.main.fieldOfView -= Input.mouseScrollDelta.y * zoomSpeed;
        Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView,zoomLimit.x,zoomLimit.y);

        #endregion

        #region mouse rotation

        // Mouse Rotation
        if (Input.GetMouseButton(0))
        {
            rotationActive = true;
            // if the game window is separate from the editor window and the editor
            // window is active then you go to right-click on the game window the
            // rotation jumps if  we don't ignore the mouseDelta for that frame.
            Vector3 mouseDelta;
            if (lastMousePosition.x >= 0 &&
                lastMousePosition.y >= 0 &&
                lastMousePosition.x <= Screen.width &&
                lastMousePosition.y <= Screen.height)
                mouseDelta = Input.mousePosition - lastMousePosition;
            else
                mouseDelta = Vector3.zero;

            var rotation = Vector3.up * Time.deltaTime * rotateSpeed * mouseDelta.x;
            //rotation += Vector3.left * Time.deltaTime * rotateSpeed * mouseDelta.y;

            transform.Rotate(rotation, Space.World);

            // Make sure z rotation stays locked
            rotation = transform.rotation.eulerAngles;
            rotation.z = 0;
            transform.rotation = Quaternion.Euler(rotation);
        }

        if (Input.GetMouseButtonUp(0))
        {
            rotationActive = false;
            transform.rotation = Quaternion.Slerp(transform.rotation, initialRot, 0.5f * Time.time);

        }

        lastMousePosition = Input.mousePosition;


        #endregion


        #region boundaries

        pos = transform.position;
        pos.y = Mathf.Clamp(pos.y,heightLimit.x,heightLimit.y);
        pos.z = Mathf.Clamp(pos.z, lenghtLimit.x, lenghtLimit.y);
        pos.x = Mathf.Clamp(pos.x, widthLimit.x, widthLimit.y);
        transform.position = pos;


        #endregion

    }

}




