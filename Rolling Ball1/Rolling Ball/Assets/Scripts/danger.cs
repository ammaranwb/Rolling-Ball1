﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class danger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ball")
        {
            other.transform.position = new Vector3(-5, 5, 5);
        }
    }
}
