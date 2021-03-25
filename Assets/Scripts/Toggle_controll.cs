using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Toggle_controll : MonoBehaviour
{
    public Toggle toggle;
    public Image Border;
    public Sprite border;
    public Sprite no_border;
    public Text title;
    public string game_name;

    void Update()
    {
        if (toggle.isOn == true)
        {
            Border.sprite = border;
            title.text = game_name;
        }

        if (toggle.isOn == false)
        {
            Border.sprite = no_border;
        }
    }
}
