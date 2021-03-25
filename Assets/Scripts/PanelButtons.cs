using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelButtons : MonoBehaviour
{
    Animator anim;
    bool panel_opened = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    public void Open_panel()
    {
        if (panel_opened == false)
        {
            anim.SetTrigger("open");
            panel_opened = true;
        }
        else
        {
          anim.SetTrigger("close");
           panel_opened = false;
        }
    }
}
