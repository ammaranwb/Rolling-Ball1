//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PlatformAttatch : MonoBehaviour
//{
//    //public float speed;

//    private Transform currentPlatform;

//    private void OnTriggerEnter(Collider other)
//    {
//        print(other);
//        if(other.transform.tag == "MovingPlatform")
//        {
//            currentPlatform = other.transform;
//            transform.parent = other.transform;
//        }
//    }

//    private void OnTriggerExit(Collider other)
//    {
//        if (other.transform == currentPlatform)
//        {
//            currentPlatform = null;
//            transform.parent = null;
//        }

//    }
//}
