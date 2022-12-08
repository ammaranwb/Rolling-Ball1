using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCollect : MonoBehaviour
{
    //public Text MyscoreText;
    //private int ScoreNum;
    //private int ScoreIs;

    // Start is called before the first frame update


    void Start()
    {
        //ScoreNum = 0;
        //MyscoreText.text = "Score: " + ScoreNum;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(90 * Time.deltaTime, 0, 0);
    }



    //private void OnTriggerEnter(Collider ball)
    //{
    //    if (ball.name == "Ball")
    //    {
    //        ScoreNum = ScoreNum + 1;
    //        //other.GetComponent<Accelerometer>().points++;
    //        Destroy(gameObject);
    //        MyscoreText.text = "Score: " + ScoreNum;


    //    }
    //}
            
}
