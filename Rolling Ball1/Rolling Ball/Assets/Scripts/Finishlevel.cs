using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Finishlevel : MonoBehaviour
{
    public int levelToUnlock;
    int numberOfunlockedLevels;
    public GameObject collisionparticle;
    public bool once = true;
    public ParticleSystem PS;
    public GameObject YouWinPanel;
    public bool Win = false;
    int yourscore;
    public Accelerometer acc;
    private int var;
    public bool wintriggered;
 

    private void Start()
    {
        collisionparticle.SetActive(false);
        acc = FindObjectOfType<Accelerometer>();
      
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Ball")
        {
            if (wintriggered)
            {
                if (AdsManager.Instance.interstitialMainMenu.IsLoaded())
                    AdsManager.Instance.ShowMainMenuInterstitial();
                numberOfunlockedLevels = PlayerPrefs.GetInt("levelsUnlocked");
                collisionparticle.SetActive(true);
                Win = true;
                if (numberOfunlockedLevels <= levelToUnlock)
                {
                    PlayerPrefs.SetInt("levelsUnlocked", numberOfunlockedLevels + 1);



                }

                var = PlayerPrefs.GetInt("totalscore");
                PlayerPrefs.SetInt("totalscore", var + acc.currentscore);



                //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                Invoke("winPanel", 1);
                Invoke("LoadNextLevel", 5);

                wintriggered = false;
            }
        }

        //if(other.gameObject.CompareTag("Ball") && once)
        //{
        //    //  var em = collisionparticle.emission;
        //    //var dur = collisionparticle.duration;
           
        //    //em.enabled = true;
        //    //collisionparticle.

        //   // once = false;
            

        //}

    }
    


    void LoadNextLevel()
    {
        CancelInvoke("winPanel");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    void winPanel()
    {
        YouWinPanel.SetActive(true);
    }

    
}

