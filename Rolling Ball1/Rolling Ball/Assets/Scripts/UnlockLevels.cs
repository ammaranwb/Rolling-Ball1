using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UnlockLevels : MonoBehaviour
{
    [SerializeField] Button[] buttons;
    int unlockedLevelsNumber;
    public Sprite unlock;
    public Sprite stars;
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("levelsUnlocked"))
        {
            PlayerPrefs.SetInt("levelsUnlocked", 1);
        }
        //PlayerPrefs.SetInt("levelsUnlocked", 6);
        unlockedLevelsNumber = PlayerPrefs.GetInt("levelsUnlocked");
        //uncmnt
        //for (int i = 0; i < buttons.Length; i++)
        //{
        //    buttons[i].interactable = false;
        //    //lockpanel.GetComponent<Image>();
        //}
        //uncmnt

        //inertacable levels should have green locks
        for (int i = 0; i < unlockedLevelsNumber; i++)
        {
            Debug.Log("turns the lock green");
            buttons[i].transform.Find("bgImage").Find("lock").GetComponent<Image>().sprite = unlock;
            //buttons[i].transform.Find("stars").GetComponent<Image>().sprite = stars;
        }

    }

    // Update is called once per frame
    void Update()
    {
        unlockedLevelsNumber = PlayerPrefs.GetInt("levelsUnlocked");
        //Debug.Log(unlockedLevelsNumber);

        for (int i = 0; i < unlockedLevelsNumber; i++)
        {
            buttons[i].interactable = true;
        }

        for(int i=1; i< 20; i++)
        {
            if (buttons[i].interactable == true)
            {
                buttons[i-1].transform.Find("stars").GetComponent<Image>().sprite = stars;
            }
        }
    }

    //public void func()
    //{
    //    for (int i = 0; i < buttons.Length; i++)
    //    {
    //        if (buttons[i].isClicked)
    //        {
    //            PM.LVLNUMBER = [i];
    //        }

    //    }
    //}
}
