using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Splash : MonoBehaviour
{
    int Splash_time = 0;
    public string SavedPassword;
    void Start()
    {
        SavedPassword = PlayerPrefs.GetString("Password");
        Splash_time = 0;

    }

    void Update()
    {
        Splash_time = Splash_time + 1;
        if (Splash_time > 200)
        {
            if (SavedPassword.Length == 6)
            {
                SceneManager.LoadScene(2);
            }
            else
            {
                SceneManager.LoadScene(1);
            }
           
        }
    }
}
