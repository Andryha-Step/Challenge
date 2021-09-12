using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rules : MonoBehaviour
{
    public GameObject toggle;
    public GameObject info;

    public void Open()
    {
        info.SetActive(true);
    }

    public void Close()
    {
        info.SetActive(false);
        toggle.SetActive(true);
    }
}
