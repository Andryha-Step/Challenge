using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Load_game : MonoBehaviour
{
    int Load_time = 0;
    void Start()
    {
        Load_time = 0;
    }

    void Update()
    {
        Load_time = Load_time + 1;
        if (Load_time > 300)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
