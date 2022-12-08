using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLANESCRIPT : MonoBehaviour
{
    public PlayerManager playerMnager;
    private bool destroyed;
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
        if(other.gameObject.tag == "Ball" && !destroyed)
        {
            destroyed = true;
            playerMnager.gameOver();
;        }
    }
}
