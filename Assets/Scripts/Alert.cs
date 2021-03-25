using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alert : MonoBehaviour
{
    public GameObject alert;

    public void al_open()
    {
        alert.SetActive(true);
    }
}
