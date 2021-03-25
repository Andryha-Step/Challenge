using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dark : MonoBehaviour
{
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Dark_panel()
    {
        anim.SetTrigger("dark");
    }
}
