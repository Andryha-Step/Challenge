using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Left_Right_anim : MonoBehaviour
{
    Animator anim;
    public int i = 0;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Left()
    {
        if (i == 0)
        {
            anim.SetTrigger("right");
            i = i + 1;
        }
        else
        {
            if (i == 1)
            {
                anim.SetTrigger("right_false");
            }
            else
            {
                anim.SetTrigger("right2");
                i = i + 1;
            }
        }
    }

    public void Right()
    {


        if (i == 0)
        {
            anim.SetTrigger("left");
            i = i - 1;
        }
        else
        {
            if (i == -1)
            {
                anim.SetTrigger("left_false");
            }
            else
            {
                anim.SetTrigger("left2");
                i = i - 1;
            }
        }
    }
}
