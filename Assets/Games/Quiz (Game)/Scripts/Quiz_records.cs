using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Quiz_records : MonoBehaviour
{
    public Text time;
    public Text rec;
    public Text max_rec_t;
    public int max_rec;
    public int record;
    public GameObject restart;


    void Start()
    {
        max_rec = PlayerPrefs.GetInt("QuizMaxRecord");
        max_rec_t.text = max_rec.ToString() + "/20";
        time.text = PlayerPrefs.GetString("QuizTime");
        record = PlayerPrefs.GetInt("QuizRecord");
        rec.text = record.ToString() + "/20";
            restart.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(13);
    }
}
