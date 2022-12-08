using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlank : MonoBehaviour
{
    public GameObject Ball;

    private void OnTriggerEnter(Collider other)
    {
        Ball.transform.parent = transform;
    }
    private void OnTriggerExit(Collider other)
    {
        Ball.transform.parent = null;
    }
}
