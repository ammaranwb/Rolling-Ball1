using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] public GameObject PauseMenuPanel;
    public  int LvlNumber;
    public GameObject[] Lvls;
    public GameObject FinishObj;
    public GameObject Ball;
    public GameObject cam;
    public GameObject startpos;
    public GameObject campos;
    int level = 1; 
    public void Start()
    {
        LvlNumber = PlayerPrefs.GetInt("slvl");

        PauseMenuPanel = GameObject.Find("Canvas").transform.Find("Pause Menu").gameObject;
        FinishObj = Lvls[LvlNumber - 1].transform.Find("Finish").gameObject;
        startpos = Lvls[LvlNumber - 1].transform.Find("startpos").gameObject;
        campos = Lvls[LvlNumber - 1].transform.Find("campos").gameObject;

        Vector3 newpos = startpos.transform.localPosition;
        Ball.transform.localPosition = newpos;

        Vector3 newcampos = campos.transform.localPosition;
        cam.transform.localPosition = newcampos;

        if (PlayerPrefs.GetInt("slvl") == 1)
        {
            Lvls[LvlNumber - 1].SetActive(true);
        }
        else if (PlayerPrefs.GetInt("slvl") == 2)
        {
            Lvls[LvlNumber - 1].SetActive(true);
        }
        else if (PlayerPrefs.GetInt("slvl") == 3)
        {
            Lvls[LvlNumber - 1].SetActive(true);
        }else if (PlayerPrefs.GetInt("slvl") == 4)
        {
            Lvls[LvlNumber - 1].SetActive(true);
        }else if (PlayerPrefs.GetInt("slvl") == 5)
        {
            Lvls[LvlNumber - 1].SetActive(true);
        }else if (PlayerPrefs.GetInt("slvl") == 6)
        {
            Lvls[LvlNumber - 1].SetActive(true);
        }else if (PlayerPrefs.GetInt("slvl") == 7)
        {
            Lvls[LvlNumber - 1].SetActive(true);
        }else if (PlayerPrefs.GetInt("slvl") == 8)
        {
            Lvls[LvlNumber - 1].SetActive(true);
        }else if (PlayerPrefs.GetInt("slvl") == 9)
        {
            Lvls[LvlNumber - 1].SetActive(true);
        }else if (PlayerPrefs.GetInt("slvl") == 10)
        {
            Lvls[LvlNumber - 1].SetActive(true);
        }
            
        


        
    }
    public void Pause()
    {
        PauseMenuPanel.SetActive(true);
        Time.timeScale = 0f;
    }
    
    public void Resume()
    {
        PauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
    }
    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SelectLevel");
    }

    public void NextLevel()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene("Level01");
        PlayerPrefs.SetInt("slvl", LvlNumber + 1);

    }
    public void CurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("i am current level");
        Time.timeScale = 1f;

    }
    public void lvl1()
    {
        SceneManager.LoadScene("Level0" + level.ToString());
        LvlNumber = 1;
    }
    public void lvl2()
    {
        SceneManager.LoadScene("Level0" + level.ToString());
        LvlNumber = 2;
    }
    



}
