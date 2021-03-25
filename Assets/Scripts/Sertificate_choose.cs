using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sertificate_choose : MonoBehaviour
{
    public Toggle toggle;
    public Image sert_small;
    public Image sert_big;
    public Text title_small;
    public Text title_big;

    public GameObject info;
    public GameObject info_butoon;
    public GameObject chose_panel;
    public GameObject main;

    void Update()
    {
        if (toggle.isOn == true)
        {
            sert_big.sprite = sert_small.sprite;
            title_big.text = title_small.text;
        }
    }

    public void choose()
    {
        chose_panel.SetActive(true);
        main.SetActive(false);
        info.SetActive(false);
    }

    public void info_open()
    {
        info_butoon.SetActive(false);
        info.SetActive(true);
    }

    public void info_close()
    {
        info_butoon.SetActive(true);
        info.SetActive(false);
    }
}
