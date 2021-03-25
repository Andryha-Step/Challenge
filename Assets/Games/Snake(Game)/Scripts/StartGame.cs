using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public float time = 5;
    public Text time_t;
    bool start = false;
    public GameObject text;

    void Start()
    {
        
    }

    void Update()
    {
        if(start == true)
        {
            text.SetActive(true);
            time = time - Time.deltaTime;
            time_t.text = Mathf.Round(time).ToString();
        }

        if (time < 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void StartGameButton()
    {
        start = true;
    }
}
