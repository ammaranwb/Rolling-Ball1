using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicToggle : MonoBehaviour
{
    [SerializeField] RectTransform uiHandleRectTransform;
    //[SerializeField] Color backgroundActiveColor;
    //[SerializeField] Color handleActiveColor;

    //Image backgroundImage, handleImage;

    //Color backgroundDefaultColor, handleDefaultColor;

    Toggle toggle;
    Vector2 handlePosition;
    public Sprite musicon;
    public Sprite musicoff;
    public GameObject handler;
    

    private void Awake()
    {
        toggle = GetComponent<Toggle>();
        handlePosition = uiHandleRectTransform.anchoredPosition;
        //backgroundImage = uiHandleRectTransform.parent.GetComponent<Image>();
        //handleImage = uiHandleRectTransform.GetComponent<Image>();

        //backgroundDefaultColor = backgroundImage.color;
        //handleDefaultColor = handleImage.color;

        toggle.onValueChanged.AddListener(OnSwitch);

        if (toggle.isOn)
        {
            OnSwitch(true);
        }

    }
    void OnSwitch(bool on)
    {
        if (on)
        {
            uiHandleRectTransform.anchoredPosition = handlePosition * -1;
            handler.transform.Find("music_bg").Find("musicOff").GetComponent<Image>().sprite = musicon;
        }
        else
        {
            uiHandleRectTransform.anchoredPosition = handlePosition;
            handler.transform.Find("music_bg").Find("musicOff").GetComponent<Image>().sprite = musicoff;

        }


        //uiHandleRectTransform.anchoredPosition = on ? handlePosition * -1 : handlePosition;
        //backgroundImage.color = on ? backgroundActiveColor : backgroundDefaultColor;
        //handleImage.color = on ? handleActiveColor : handleDefaultColor;
    }
    void OnDestroy()
    {
        toggle.onValueChanged.RemoveListener(OnSwitch);
    }

}
