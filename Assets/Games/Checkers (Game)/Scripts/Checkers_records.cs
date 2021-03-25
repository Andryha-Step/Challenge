using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Checkers_records : MonoBehaviour
{
    public Text time;
    public Text resalt;
    public Text min_time_t;
    public Text record;

    public GameObject restart;


    void Start()
    {
        resalt.text = PlayerPrefs.GetString("CheckersResalt");
        min_time_t.text = PlayerPrefs.GetString("CheckersMinTimeText");
        time.text = PlayerPrefs.GetString("CheckersTime");
        record.text = PlayerPrefs.GetString("CheckersRecord");
            restart.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(7);
    }
}
