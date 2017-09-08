using UnityEngine;

[RequireComponent(typeof(Camera))]

public class RTSCameraController : MonoBehaviour {

    public float ScreenEdgeBorderThickness = 5.0f;
    [Space]
    [Header("Speeds")]
    public float moveSpeed = 50.0f;
    
    public float rotateSpeed;
    public float zoomSpeed;
    [Space]
    [Header("Movement Limits")]
    public Vector2 heightLimit;
    public Vector2 lenghtLimit;
    public Vector2 widthLimit;

    private Vector3 initialPos;
    private Vector3 panTranslation;
    private Vector3 pos;

    

    // Use this for initialization
    void Start () {
        initialPos = transform.position;
	}
	
	
	void Update () {

            panTranslation = Vector3.zero;

            if (Input.GetKey(KeyCode.W) || Input.mousePosition.y >= Screen.height - ScreenEdgeBorderThickness)
            {
                panTranslation += Vector3.forward * moveSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.S) || Input.mousePosition.y <= ScreenEdgeBorderThickness)
            {
                panTranslation -= Vector3.forward * moveSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.A) || Input.mousePosition.x <= ScreenEdgeBorderThickness)
            {
                panTranslation += Vector3.left * moveSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - ScreenEdgeBorderThickness)
            {
                panTranslation += Vector3.right * moveSpeed * Time.deltaTime;
                //pos.x += moveSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.Q))
            {
                panTranslation += Vector3.up * moveSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.E))
            {
                panTranslation += Vector3.down * moveSpeed * Time.deltaTime;
            }

            transform.Translate(panTranslation, Space.World);

            Camera.main.fieldOfView -= Input.mouseScrollDelta.y * zoomSpeed;
            Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, 25,70);
       

        #region boundaries

        pos = transform.position;
        pos.y = Mathf.Clamp(pos.y,heightLimit.x,heightLimit.y);
        pos.z = Mathf.Clamp(pos.z, lenghtLimit.x, lenghtLimit.y);
        pos.x = Mathf.Clamp(pos.x, widthLimit.x, widthLimit.y);
        transform.position = pos;


        #endregion

    }

}






/*
 * 
 * #region mouse rotation

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

        }

        lastMousePosition = Input.mousePosition;


        #endregion
 * 
 * */





