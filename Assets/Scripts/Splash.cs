using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Splash : MonoBehaviour
{
    public string SavedPassword;
    float Splash_time = 4;

    void Start()
    {
        SavedPassword = PlayerPrefs.GetString("Password");
        Splash_time = 4;

    }

    void Update()
    {
        Splash_time = Splash_time - Time.deltaTime;
        if (Splash_time < 0)
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
