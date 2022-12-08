using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class First_Screen_Handle : MonoBehaviour
{
    public GameObject QuitGamePanel, RateusPanel, LevelsPanel, SettingsPanel, MusicPanel;
    public GameObject Panel1;
    public AudioSource music;
    public GameObject Mison, Misoff;
    public GameObject keysbtn, accbtn;
    public Text TotalScore;
    int total;
    public Finishlevel fl;


    public void Start()
    {
        

        music = FindObjectOfType<AudioSource>();
        fl = FindObjectOfType<Finishlevel>();

        if (PlayerPrefs.GetString("isPlaying") == "true")
        {
            //music.Play();
            musicOn();
        }
        else
        {
            musicOff();
        }
            

        if(PlayerPrefs.GetInt("activecontroller") == 1)
        {
            keycontrols();
        }
        else
        {
            accelerometer();
        }

        if (!PlayerPrefs.HasKey("totalscore"))
        {
            
            TotalScore.text = "" + 0;
        }
        else
        {
            WinCoin();
        }

    }

    public void keycontrols()
    {
        PlayerPrefs.SetInt("activecontroller", 1);

        keysbtn.transform.localScale = new Vector3(1.25f, 1.25f, 1f);
        accbtn.transform.localScale = new Vector3(1f, 1f, 1f);
    }
    public void accelerometer()
    {
        PlayerPrefs.SetInt("activecontroller", 0);
        accbtn.transform.localScale = new Vector3(1.25f, 1.25f, 1f);
        keysbtn.transform.localScale = new Vector3(1f, 1f, 1f);
    }
    public void Quit()
    {
        QuitGamePanel.SetActive(true);
        Panel1.SetActive(false);

    }
    public void Music()
    {
        MusicPanel.SetActive(true);
        Panel1.SetActive(false);

    }
    public void MusicBack()
    {
        MusicPanel.SetActive(false);
        Panel1.SetActive(true);

    }

    public void Rate()
    {
        RateusPanel.SetActive(true);
        Panel1.SetActive(false);

    }
    public void Play()
    {
        LevelsPanel.SetActive(true);
        Panel1.SetActive(false);

    }
    public void Settings()
    {
        SettingsPanel.SetActive(true);
        Panel1.SetActive(false);

    }
    public void back()
    {
        SettingsPanel.SetActive(false);
        Panel1.SetActive(true);

    }

    public void QuitNo()
    {
        QuitGamePanel.SetActive(false);
        Panel1.SetActive(true);
    }
    public void RateNoT()
    {
        RateusPanel.SetActive(false);
        Panel1.SetActive(true);
    }
    public void QuitYes()
    {
        Debug.Log("game ended");
        Application.Quit();
    }
    public void RateYes()
    {
        Debug.Log("you have been taken to play store");
    }
    public void MenuSelect()
    {
        SceneManager.LoadScene("SelectLevel");
    }

    public void musicOn()
    {
        PlayerPrefs.SetString("isPlaying", "true");
        music.Play();
        //GO.transform.Find("Image").Find("off").localScale = new Vector3(0.92f, 0.92f, 0.92f);
        Mison.transform.localScale = new Vector3(1.25f, 1.25f, 1f);
        Misoff.transform.localScale = new Vector3(0.75f, 0.75f, 1f);
    }
    public void musicOff()
    {
        PlayerPrefs.SetString("isPlaying", "false");
        music.Stop();
        Mison.transform.localScale = new Vector3(0.75f, 0.75f, 1f);
        Misoff.transform.localScale = new Vector3(1.25f, 1.25f, 1f);
    }
    public void MenuBack()
    {
        LevelsPanel.SetActive(false);
        Panel1.SetActive(true);
    }
    public void WinCoin()
    {
        total = PlayerPrefs.GetInt("totalscore");
        TotalScore.text = "" + total;
    }
}