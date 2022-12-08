using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerCount : MonoBehaviour
{
    public GameObject textDisplay;
    public int secondsLeft = 50;
    public bool takingAway = false;
    public Finishlevel FLObj;
    public Accelerometer acc;
    public GameOver Gameovr;
    public GameObject GameOverPanel;
    //public bool Finish = false;
    public PauseMenu PMenuScript;


    void Start()
    {
        if (PlayerPrefs.GetInt("slvl") == 5 || PlayerPrefs.GetInt("slvl") == 6)
        {
            secondsLeft = 80;
        }
        if (PlayerPrefs.GetInt("slvl") == 7 || PlayerPrefs.GetInt("slvl") == 8)
        {
            secondsLeft = 90;
        }
        if (PlayerPrefs.GetInt("slvl") == 9 || PlayerPrefs.GetInt("slvl") == 10)
        {
            secondsLeft = 100;
        }
            PMenuScript = FindObjectOfType<PauseMenu>();
        textDisplay.GetComponent<Text>().text = "00:" + secondsLeft;
        FLObj = PMenuScript.FinishObj.GetComponent<Finishlevel>();
        Gameovr = FindObjectOfType<GameOver>();
        acc = FindObjectOfType<Accelerometer>();
    }

     void Update()
    {
       
        if (!takingAway && secondsLeft > 0 && !FLObj.Win && !Gameovr.fail && !acc.fail)
        {
            StartCoroutine(TimerTake());
        }
        else if(secondsLeft == 0)
        {
            GameOverPanel.SetActive(true);
            //textDisplay.GetComponent<Text>().text.enable = false;
            //canvas.Timer = false;
        }
        

        
    }
    IEnumerator TimerTake()
    {
        takingAway = true;
        yield return new WaitForSeconds(1);
        secondsLeft -= 1;
        if (secondsLeft < 10)
        {
            textDisplay.GetComponent<Text>().text = "00:0" + secondsLeft;
        }
        else
        {
            textDisplay.GetComponent<Text>().text = "00:" + secondsLeft;
        }
        takingAway = false;
    }

    
   
}
