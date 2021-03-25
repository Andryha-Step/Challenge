using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Home_Dropdown : MonoBehaviour
{
    public Text[] ItemT;
    public Text mainText;

    bool opened = false;

    public GameObject list;

    void Start()
    {
        list.SetActive(false);
    }

    public void Item_bttn(int index)
    {
        mainText.text = ItemT[index].text;
    }

    public void Open_Close()
    {
        if (opened == false)
        {
            list.SetActive(true);
            opened = true;
        }
            
        else if (opened == true)
        {
            list.SetActive(false);
            opened = false;
        }
            
    }
}
