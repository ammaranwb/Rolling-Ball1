using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class LevelSelector : MonoBehaviour
{

    //public RectTransform Button;
    public int level;
    public GameObject lockobj;
    public UnlockLevels unlocklevelsobj;
    public GameObject levels;
    //public PauseMenu PM;

    // Start is called before the first frame update
    void Start()
    {

        //Button.GetComponent<Animator>().Play("Hover off");
        //PM = FindObjectOfType<PauseMenu>();
        
    }


    public void OpenScene1()
    {
        //print("Level0" + level.ToString());
        //if(level < 10)
        //{
        //    SceneManager.LoadScene("Level0" + level.ToString());
        //}
        //else
        //{
        //    SceneManager.LoadScene("Level" + level.ToString());
        //}
        SceneManager.LoadScene("Level01");
        PlayerPrefs.SetInt("slvl", 1);
    }
    public void OpenScene2()
    {
        SceneManager.LoadScene("Level01");
        PlayerPrefs.SetInt("slvl", 2);
    }public void OpenScene3()
    {
        SceneManager.LoadScene("Level01");
        PlayerPrefs.SetInt("slvl", 3);
    }public void OpenScene4()
    {
        SceneManager.LoadScene("Level01");
        PlayerPrefs.SetInt("slvl", 4);
    }public void OpenScene5()
    {
        SceneManager.LoadScene("Level01");
        PlayerPrefs.SetInt("slvl", 5);
    }public void OpenScene6()
    {
        SceneManager.LoadScene("Level01");
        PlayerPrefs.SetInt("slvl", 6);
    }public void OpenScene7()
    {
        SceneManager.LoadScene("Level01");
        PlayerPrefs.SetInt("slvl", 7);
    }public void OpenScene8()
    {
        SceneManager.LoadScene("Level01");
        PlayerPrefs.SetInt("slvl", 8);
    }public void OpenScene9()
    {
        SceneManager.LoadScene("Level01");
        PlayerPrefs.SetInt("slvl", 9);
    }public void OpenScene10()
    {
        SceneManager.LoadScene("Level01");
        PlayerPrefs.SetInt("slvl", 10);
    }



    /* public void OnPointerEnter(PointerEventData eventData)
    {
        Button.GetComponent<Animator>().Play("Hover");
        Debug.Log("on the pointer");
    }



    public void OnPointerExit(PointerEventData eventData)
    {
        Button.GetComponent<Animator>().Play("Hover off");
    }*/

}
