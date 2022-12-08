using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bucket : MonoBehaviour
{
    Rigidbody rigid;
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name.Equals("Ball"))
            rigid.isKinematic = false;
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name.Equals("Ball"))
            Debug.Log("ball destryoed");
    }
}
