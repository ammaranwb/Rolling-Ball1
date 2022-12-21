using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    [SerializeField] public GameObject GameoverPanel;
    public bool fail= false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Ball")
        {
            if (AdsManager.Instance.interstitialMainMenu.IsLoaded())
                AdsManager.Instance.ShowMainMenuInterstitial();
            GameoverPanel.SetActive(true);
            fail = true;
        }
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SelectLevel");
    }
}
