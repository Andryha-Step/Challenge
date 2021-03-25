using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alert_close : MonoBehaviour
{
    public GameObject alert;

    public void al_close()
    {
        alert.SetActive(false);
    }
}
