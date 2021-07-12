using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Left_Right_anim : MonoBehaviour
{
    Animator anim;
    public int i;

    public Text text;

    public string[] values;
    public string[] values_2;
    public static bool cheeches;
    public bool use_2 = false;

    float time = 0.2f;
    bool time_start = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        text.text = values[i];
        time_start = false;
        cheeches = false;
    }

    void Update()
    {
        

        if (time_start == true)
        {
            time -= Time.deltaTime;
        }
        if (time < 0)
        {
            time_start = false;
            if (cheeches == true && use_2 == true)
            {
                text.text = values_2[i];
            }
            else
            {
                text.text = values[i];
            }
            time = 0.2f;
        }
    }

    public void Left()
    {
        
        if (i != 0)
        {
            i -= 1;
            time_start = true;
            anim.SetTrigger("left");
        }
        else
        {
            anim.SetTrigger("false");
        }
    }

    public void Right()
    {
        if (cheeches == true && use_2 == true)
        {
            if (i != values_2.Length - 1)
            {
                i += 1;
                time_start = true;
                anim.SetTrigger("right");
            }
            else
            {
                anim.SetTrigger("false");
            }
        }
        else
        {
            if (i != values.Length - 1)
            {
                i += 1;
                time_start = true;
                anim.SetTrigger("right");
            }
            else
            {
                anim.SetTrigger("false");
            }
        }
        
    }
}
