using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scrolls : MonoBehaviour
{
    public Scrollbar scrollView;
    public Scrollbar scroll;

    void Start()
    {
        
    }

    void Update()
    {
        scroll.value = scrollView.value;
    }
}
