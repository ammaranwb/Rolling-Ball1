using UnityEngine;

public class CamerafOLLOW : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float smoothSpeed = 0.125f;
    //public bool lookAtTarget = false;
    //public Rigidbody rigidbody;
    Quaternion wantedRotation;
    //public GameObject player;

    //code2
    public Transform Player;
    public float distanceFromObject = 3f;

    void Start()
    {
        //offset = transform.position - target.transform.position;
        //rigidbody = GetComponent<Rigidbody>();
    }

    void LateUpdate()
    {
        //transform.position = target.transform.position + offset;
        //MovePlayerRelativeToCamera();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Vector3 flatSpeed = player.Rigidbody.velocity;
        //flatSpeed.y = 0;

        ////stores the flat velocity of your player so it can put the camera always behind it



        //if (flatSpeed != Vector3.zero)
        //{
        //    float targetAngle = Quaternion.LookRotation(flatSpeed).eulerAngles.y;
        //    wantedRotation = Quaternion.Euler(0, targetAngle, 0);
        //}

        //transform.position = target.transform.position + (wantedRotation * offset);
        //transform.LookAt(target.transform);



        //Vector3 newPosition = targetObject.transform.position + cameraOffset;
        //transform.position = newPosition;
        //transform.position = Vector3.Slerp(transform.position, newPosition, smoothFactor);

        //Vector3 desiredPosition = target.transform.position + offset;
        //Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        //transform.position = smoothedPosition;



        //if(Input.GetMouseButton(0))
        //{
        //    transform.RotateAround(target.position, transform.up, Input.GetAxis("Mouse X"));
        //    transform.RotateAround(target.position, transform.right, Input.GetAxis("Mouse Y"));
        //}
        //transform.LookAt(target.position);

        /*if (lookAtTarget)
        {
          transform.LookAt(targetObject);
       } */
        //MovePlayerRelativeToCamera();


        //code2
        Vector3 lookOnObject = Player.position - transform.position;
        transform.forward = lookOnObject.normalized;
        Vector3 playerLastPosition;
        playerLastPosition = Player.position - lookOnObject.normalized * distanceFromObject;
        playerLastPosition.y = Player.position.y + distanceFromObject / 2;
        transform.position = playerLastPosition;
        Vector3 lookOnObject1 = Player.position - transform.position;
        transform.forward = lookOnObject1.normalized;
        Vector3 playerLastPosition1;
        playerLastPosition1 = Player.position - lookOnObject1.normalized * distanceFromObject;
        playerLastPosition1.y = Player.position.y + distanceFromObject / 2;
        transform.position = playerLastPosition1;
        // transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * smoothSpeed, Space.World);
    }

    void MovePlayerRelativeToCamera()
    {
        float playerVerticalInput = Input.GetAxis("Vertical");
        float playerHorizontalInput = Input.GetAxis("Horizontal");

        Vector3 forward = transform.InverseTransformVector(Camera.main.transform.forward);
        Vector3 right = transform.InverseTransformVector(Camera.main.transform.right);
        forward.y = 0;
        forward.y = 0;
        forward = forward.normalized;
        right = right.normalized;

        Vector3 forwardRelativeVerticalInput = playerVerticalInput * forward;
        Vector3 rightRelativeVerticalInput = playerHorizontalInput * right;

        Vector3 cameraRelativeMovement = forwardRelativeVerticalInput + rightRelativeVerticalInput;

        transform.Translate(cameraRelativeMovement);
    }

    void cameraMovesBck()
    {
        Vector3 pos = new Vector3(0, 0, 0);
    }
}
