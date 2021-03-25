using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropdown : MonoBehaviour
{
    public GameObject DropDown;
    public GameObject StartPanel;

    public void DD_open()
    {
        DropDown.SetActive(true);
        StartPanel.SetActive(false);
    }

    public void DD_close()
    {
        DropDown.SetActive(false);
        StartPanel.SetActive(true);
    }
}
