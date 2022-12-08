using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Accelerometer : MonoBehaviour
{
    public bool isFlat = true;
    private Rigidbody rigid;
    public float speed = 5f;
    public bool isGrounded;
    //public int points;
    //public Vector3 startpos;
    public float jumpspeed;
    public Text MyscoreText;
    private int ScoreNum, random;
    public GameObject gameoverPanel, keycontrolspanel;
    private Touch touch;
    public Camera MainCam;
    int isMovinghor;
    public bool fail = false;
    public int currentscore;


    private Quaternion calibrationQuaternion;

    private void Start()
    {
        //CalibrateAccelerometer();

        rigid = GetComponent<Rigidbody>();
        isGrounded = false;
        PlayerPrefs.SetInt("LevelsUnlocked", 1);
        ScoreNum = 0;
        MyscoreText.text = "" + ScoreNum;
        /*if (PlayerPrefs.GetString("isPlaying") == "true")
        {
            Debug.Log("music is playing");
            //music = GetComponent<AudioSource>();
            //music.Play();
            //music.GetComponent<AudioSource>().enabled() = true;
        }
        else
        {
            Debug.Log("music is off");
        }*/

        if (!PlayerPrefs.HasKey("activecontroller"))
        {
            PlayerPrefs.SetInt("activecontroller", 0);
            random = 1;
        }

        if (PlayerPrefs.GetInt("activecontroller") == 1)
        {
            keycontrolspanel.SetActive(true);
            //acceleeometer should turn off
            random = 0;
            Debug.Log("Keys are working, accelerometer is turned off");
            //keysbtn.transform.localScale = new Vector3(1.25f, 1.25f, 1f);
        }
        else
        {
            if (PlayerPrefs.GetInt("activecontroller") == 0)
            {
                keycontrolspanel.SetActive(false);
                //acceleeometer should turn on
                random = 1;
                Debug.Log("Keys are off, accelerometer is working");
                //accbtn.transform.localScale = new Vector3(1.25f, 1.25f, 1f);
            }
        }

        if (Input.touchSupported)
            jumpspeed = 650;
        else
            jumpspeed = 800;

        
    }
    /* private void FixedUpdate()
    {
        
    }*/
    //void CalibrateAccelerometer()
    //{
    //    Vector3 accelerationSnapshot = Input.acceleration;

    //    Quaternion rotateQuaternion = Quaternion.FromToRotation(
    //        new Vector3(0.0f, 0.0f, -1.0f), accelerationSnapshot);

    //    calibrationQuaternion = Quaternion.Inverse(rotateQuaternion);
    //}

    private void FixedUpdate()
    {
        //if level 1 Gameobject is selected
        //takes pos of startpos
        //assigns pos of startpos to rigid.position
        //ACCELEROMETER
        if (random == 1)
        {
            Vector3 tilt = Input.acceleration;
            //Vector3 fixedAcceleration = calibrationQuaternion * theAcceleration;
            // print(Input.GetAxis("Horizontal"));
            print(tilt);
            if (tilt.x >= 0f)
            {
                rigid.AddForce(MainCam.transform.right * speed * tilt.x);
            }
            else if (tilt.x < 0)
            {
                rigid.AddForce(-MainCam.transform.right * speed * (-tilt.x));
            }
            if (tilt.y >= -0.4)
            {
                rigid.AddForce(MainCam.transform.forward * speed * (tilt.y+.4f));
            }
            else if (tilt.y < -.4)
            {
                rigid.AddForce(-MainCam.transform.forward * speed * (-tilt.y-.3f));
            }
        }
        //Vector3 tilt = Input.acceleration;





        //BALL MOVES WITH KEYS
        if (isFlat)
        { }

        //BALL JUMPS HERE WITH SPACE
        if (isGrounded == true && Input.GetKeyUp(KeyCode.Space))
        {
            rigid.AddForce(new Vector3(0f, 500f, 0f));
            //Debug.Log("I am working");
        }


        if (isMovinghor == 1)
        {
            //MoveLeft();
            rigid.AddForce(-MainCam.transform.right * speed);
        }
        else if (isMovinghor == 2)
        {
            rigid.AddForce(MainCam.transform.right * speed);
        }
        else if (isMovinghor == 3)
        {
            rigid.AddForce(MainCam.transform.forward * speed);
        }
        else if (isMovinghor == 4)
        {
            rigid.AddForce(-MainCam.transform.forward * speed);
        }
        

    }



    //CHECKS IF BALL IS TOUCHING THE GROUND OR NOT

    //BALL IS TOUCHING THE GROUND


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "obstacle")
        {
            Debug.Log("destroyed the ball");
            //transform.position = startpos;
            gameoverPanel.SetActive(true);
            fail = true;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Jumptag")
        {
            isGrounded = true;
        }

    }
    //BALL IS IN THE AIR
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Jumptag")
        {
            isGrounded = false;
        }
    }


    /* private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 20), "Score: " + points);
    }*/


    //BALL JUMPS ON BUTTON CLICK
    public void jumpButton()
    {
        if (isGrounded)
        {
            //print("Jumping" + jumpspeed);
            rigid.AddForce(new Vector3(0f, jumpspeed, 0f));
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MyCoin")
        {
            ScoreNum = ScoreNum + 1;
            //other.GetComponent<Accelerometer>().points++;
            Destroy(other.gameObject);
            MyscoreText.text = "" + ScoreNum;

            currentscore = currentscore + 1;
            
        }


        if (other.tag == "bullet")
        {
            gameoverPanel.SetActive(true);
            fail = true;

        }
    }

    public void MoveLeft()
    {
        Debug.Log("go left");
        isMovinghor = 1;
       
        //transform.Translate(-Vector3.right * speed);
        //rigid.AddForce(-Vector3.right * speed);
        //rigid.transform.Translate(Force * Time.deltaTime, 0, 0);
        
    }

    public void MoveRight()
    {
        Debug.Log("go right");
        //rigid.AddForce(-Vector3.right * speed);
        // if (Input.GetAxis("Horizontal") < 0.3);
       
        
        isMovinghor = 2;
    }
    public void Moveforward()
    {
        Debug.Log("go forward");
        //rigid.AddForce(-Vector3.right * speed);
        // if (Input.GetAxis("Horizontal") < 0.3)
        isMovinghor = 3;
     

    }
    public void MoveBackward()
    {
        Debug.Log("go backward");
        //rigid.AddForce(-Vector3.right * speed);
        // if (Input.GetAxis("Horizontal") < 0.3)
        isMovinghor = 4;
     
    }

    public void noForce()
    {
        //rigid.AddForce(0, 0, -speed);
        isMovinghor = 0;
        print("hi");

    }
}
