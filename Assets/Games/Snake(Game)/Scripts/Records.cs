using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Records : MonoBehaviour
{
    public Text time;
    public Text rec;
    public Text max_rec;
    public int record;
    public GameObject restart;


    void Start()
    {
        max_rec.text = PlayerPrefs.GetString("SnakeMaxRecordText");
        time.text = PlayerPrefs.GetString("SnakeTime");
        record = PlayerPrefs.GetInt("SnakeRecord");
        rec.text = record.ToString();
        restart.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(9);
    }
}
