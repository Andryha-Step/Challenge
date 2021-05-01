using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TournamentTime : MonoBehaviour
{
    public Text[] timeText = new Text[6];
    public int[] timeNum = new int[6];
    string id;
    public GameObject leftTime;
    public Text leftTimeText;
    public int NowHour;
    public int NowMinute;
    bool plus;

    int HourLeft;
    int MinuteLeft;

    void Start()
    {
        id = PlayerPrefs.GetString("TimeID");
        if (id == "")
        {
            leftTime.SetActive(false);
        }
        else
        {
            leftTime.SetActive(true) ;
        }
        
        if (PlayerPrefs.GetInt("PlusTimeBool") == 0)
        {
            plus = false;
        }
        else
        {
            plus = true ;
        }

    }

    void Update()
    {

        for (int i = 0; i<6; i++)
        {
            if (id != "" && i != Int32.Parse(id))
            {
                timeText[i].color = Color.gray;
            }
            else if (id != "")
            {
                timeText[i].color = Color.black;
            }
        }

        var currentDate = System.DateTime.Now;
        NowHour = Int32.Parse(currentDate.Hour.ToString());
        NowMinute = Int32.Parse(currentDate.Minute.ToString());
        MinuteLeft = 60 - NowMinute;

        if (plus == true)
        {
            HourLeft = (timeNum[Int32.Parse(id)]- NowHour)-1;
        }
        else 
        {
            HourLeft = 23 - NowHour + timeNum[Int32.Parse(id)];
        }

        if (MinuteLeft < 10)
        {
            leftTimeText.text = HourLeft + ":0" + MinuteLeft;
        }
        else
        {
            leftTimeText.text = HourLeft + ":" + MinuteLeft;
        }

        PlayerPrefs.SetString("TimeID", id);
        
    }

    public void ChooseTime(int index)
    {
        if (id != "" && index == Int32.Parse(id))
        {
            id = "";
            leftTime.SetActive(false);
            timeText[index].color = Color.gray;
        }
        else
        {
            id = index.ToString();
            leftTime.SetActive(true);
            timeText[index].color = Color.black;
            if (NowHour < timeNum[index])
            {
                plus = true;
                PlayerPrefs.SetInt("PlusTimeBool", 1);
            }
            else
            {
                plus = false;
                PlayerPrefs.SetInt("PlusTimeBool", 0);
            }
        }


    }
}
