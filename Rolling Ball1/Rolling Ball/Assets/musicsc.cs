using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicsc : MonoBehaviour
{
    AudioSource music;
    public static GameObject instance;
    // Start is called before the first frame update
    void Start()
    {
        if (instance)
            Destroy(this.gameObject);
        else
            instance = this.gameObject;
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //print(PlayerPrefs.GetString("isPlaying"));
        //if (PlayerPrefs.GetString("isPlaying") == "true")
        //{
        //    Debug.Log("music is playing");
        //    music = GetComponent<AudioSource>();
        //    music.Play();
        //    //music.GetComponent<AudioSource>().enabled() = true;
        //}
        //else
        //{
        //    Debug.Log("music is off");
        //    music = GetComponent<AudioSource>();
        //    music.Stop();
        //}
    }
}
