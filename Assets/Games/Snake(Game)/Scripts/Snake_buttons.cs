using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Snake_buttons : MonoBehaviour
{
    public Image But1;
    public Image But2;
    public Image But3;

    public Sprite but_t;
    public Sprite but_f;

    public GameObject Stick1;
    public GameObject Stick2;
    public GameObject Stick3;

    public void Button1()
    {
        But1.sprite = but_t;
        But2.sprite = but_f;
        But3.sprite = but_f;

        Stick1.SetActive(true);
        Stick2.SetActive(false);
        Stick3.SetActive(false);
    }

    public void Button2()
    {
        But1.sprite = but_f;
        But2.sprite = but_t;
        But3.sprite = but_f;

        Stick1.SetActive(false);
        Stick3.SetActive(true);
        Stick2.SetActive(false);
    }

    public void Button3()
    {
        But1.sprite = but_f;
        But2.sprite = but_f;
        But3.sprite = but_t;

        Stick1.SetActive(false);
        Stick3.SetActive(false);
        Stick2.SetActive(true);
    }
}
