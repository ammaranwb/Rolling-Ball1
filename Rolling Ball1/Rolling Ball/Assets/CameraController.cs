//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class CameraController : MonoBehaviour
//{
//    [SerializeField] private Camera cam;
//    [SerializeField] private Transform target;
//    [SerializeField] private float distanceToTarget = 4;

//    private Vector2 startTouchPosition;
//    private Vector2 endTouchPosition;
//    bool isallowed = true;

//    private Vector3 previousPosition,newPosition,direction;
//    void Update()
//    {

//        //Drag
//        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
//        {
//            startTouchPosition = Input.GetTouch(0).position;
//            print(startTouchPosition);
//        }
//        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
//        {
//            endTouchPosition = Input.GetTouch(0).position;
//            print(endTouchPosition);
//        }

//        //if (endTouchPosition.x < startTouchPosition.x || endTouchPosition.x > startTouchPosition.x)

//        if(Input.GetMouseButtonDown(0))
//        {
//            // Will be true only in the 1st frame in which it detects the mouse is down (or a tap is happening)
//            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
//            print(previousPosition);
//        }
//        //else if (endTouchPosition.x < startTouchPosition.x)
//        else if (Input.GetMouseButton(0))
//        {
//             newPosition = cam.ScreenToViewportPoint(Input.mousePosition);
//             direction = previousPosition - newPosition;

//            float rotationAroundYAxis = -direction.x * 360; // camera moves horizontally
//            float rotationAroundXAxis = direction.y * -30; // camera moves vertically

//            cam.transform.position = target.position;
//            cam.transform.Rotate(new Vector3(1, 0, 0), rotationAroundXAxis);
//            cam.transform.Rotate(new Vector3(0, 1, 0), rotationAroundYAxis, Space.World); // <— This is what makes it work!

//            cam.transform.Translate(new Vector3(0, 0, -distanceToTarget));

//            previousPosition = newPosition;
//        }
//        else
//        {
//            newPosition = cam.ScreenToViewportPoint(Input.mousePosition);
//            direction = previousPosition - newPosition;

//            float rotationAroundYAxis = -direction.x * 360; // camera moves horizontally
//            float rotationAroundXAxis = direction.y * -30; // camera moves vertically
            
//            cam.transform.position = target.position;
//            cam.transform.Rotate(new Vector3(1, 0, 0), rotationAroundXAxis);
//            cam.transform.Rotate(new Vector3(0, 1, 0), rotationAroundYAxis, Space.World); // <— This is what makes it work!

//            cam.transform.Translate(new Vector3(0, 0, -distanceToTarget));

//            previousPosition = newPosition;

//        }
//    }
//}


////(Input.GetMouseButton(0)