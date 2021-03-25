using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Password : MonoBehaviour
{
    
    public string password;
    public string PasswordIn;

    public Image Im1;
    public Image Im2;
    public Image Im3;
    public Image Im4;
    public Image Im5;
    public Image Im6;
    public Sprite pass_t;
    public Sprite pass_f;


    void Start()
    {
        password = PlayerPrefs.GetString("Password");
        PasswordIn = "";
    }

    void Update()
    {
        if (PasswordIn.Length == 6)
        {
            if (PasswordIn == password)
            {
                SceneManager.LoadScene(3);
            }
            else
            {
                PasswordIn = "";
                Im1.sprite = pass_f;
                Im2.sprite = pass_f;
                Im3.sprite = pass_f;
                Im4.sprite = pass_f;
                Im5.sprite = pass_f;
                Im6.sprite = pass_f;
            }
        }

        if (PasswordIn.Length == 1)
        {
            Im1.sprite = pass_t;
        }

        if (PasswordIn.Length == 2)
        {
            Im2.sprite = pass_t;
        }

        if (PasswordIn.Length == 3)
        {
            Im3.sprite = pass_t;
        }

        if (PasswordIn.Length == 4)
        {
            Im4.sprite = pass_t;
        }

        if (PasswordIn.Length == 5)
        {
            Im5.sprite = pass_t;
        }

        if (PasswordIn.Length == 6)
        {
            Im6.sprite = pass_t;
        }

    }

    public void One()
    {
        PasswordIn = PasswordIn + "1";
    }

    public void Two()
    {
        PasswordIn = PasswordIn + "2";
    }

    public void Three()
    {
        PasswordIn = PasswordIn + "3";
    }

    public void Four()
    {
        PasswordIn = PasswordIn + "4";
    }
    
    public void Five()
    {
        PasswordIn = PasswordIn + "5";
    }

    public void Six()
    {
        PasswordIn = PasswordIn + "6";
    }

    public void Seven()
    {
        PasswordIn = PasswordIn + "7";
    }

    public void Eight()
    {
        PasswordIn = PasswordIn + "8";
    }

    public void Nine()
    {
        PasswordIn = PasswordIn + "9";
    }

    public void Zero()
    {
        PasswordIn = PasswordIn + "0";
    }
}
