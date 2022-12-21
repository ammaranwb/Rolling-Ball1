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
        }if (PlayerPrefs.GetInt("slvl") == 1 || PlayerPrefs.GetInt("slvl") == 2 || PlayerPrefs.GetInt("slvl") == 3 || PlayerPrefs.GetInt("slvl") == 4)
        {
            secondsLeft = 50;
        }
        if (PlayerPrefs.GetInt("slvl") == 7 || PlayerPrefs.GetInt("slvl") == 8 || PlayerPrefs.GetInt("slvl") == 13 )
        {
            secondsLeft = 90;
        }
        if (PlayerPrefs.GetInt("slvl") == 9 || PlayerPrefs.GetInt("slvl") == 10 || PlayerPrefs.GetInt("slvl") == 14)
        {
            secondsLeft = 100;
        }if (PlayerPrefs.GetInt("slvl") == 11)
        {
            secondsLeft = 80;
        }if (PlayerPrefs.GetInt("slvl") == 17 || PlayerPrefs.GetInt("slvl") == 18)
        {
            secondsLeft = 210;
        }
        if (PlayerPrefs.GetInt("slvl") == 19 || PlayerPrefs.GetInt("slvl") == 20 )
        {
            secondsLeft = 250;
        }
        if(PlayerPrefs.GetInt("slvl") == 16)
        {
            secondsLeft = 280;
        }
        if (PlayerPrefs.GetInt("slvl") == 12 || PlayerPrefs.GetInt("slvl") == 15)
        {
            secondsLeft = 120;
        }
            PMenuScript = FindObjectOfType<PauseMenu>();

        System.TimeSpan t = System.TimeSpan.FromSeconds(secondsLeft);
        string answer = string.Format("{1:D2}:{2:D2}",
            t.Hours,
            t.Minutes,
            t.Seconds,
            t.Milliseconds);
        textDisplay.GetComponent<Text>().text = answer;


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
        //if (secondsLeft > 60)
        //{
        //    //show in minutes

        //     System.TimeSpan t = System.TimeSpan.FromSeconds(secondsLeft);
        //    //textDisplay.GetComponent<Text>().text = string.Format("{1:D2}m:{2:D2}s", t.Minutes, t.Seconds);
        //    string answer = string.Format("{0:D2}h:{1:D2}m:{2:D2}s:{3:D3}ms",
        //        t.Hours,
        //        t.Minutes,
        //        t.Seconds,
        //        t.Milliseconds);

        //}
        //  else  if (secondsLeft <= 60 || secondsLeft >= 10)
        //{
        //    textDisplay.GetComponent<Text>().text = "00:" + secondsLeft;
        //}
        //else if (secondsLeft < 10)
        //{
        //    textDisplay.GetComponent<Text>().text = "00:0" + secondsLeft;
        //}
        System.TimeSpan t = System.TimeSpan.FromSeconds(secondsLeft);
        string answer = string.Format("{1:D2}:{2:D2}",
            t.Hours,
            t.Minutes,
            t.Seconds,
            t.Milliseconds);
        textDisplay.GetComponent<Text>().text = answer;

        takingAway = false;
    }

    
   
}
