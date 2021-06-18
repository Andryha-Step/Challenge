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
    public int NowSec;
    public bool plus;

    int HourLeft;
    int MinuteLeft;
    int SecLeft;
    

    void Start()
    {
        var currentDate = System.DateTime.Now;

        id = PlayerPrefs.GetString("TimeID");
        if (id == "")
        {
            leftTime.SetActive(false);
        }
        else
        {
            leftTime.SetActive(true) ;
        }

        if (PlayerPrefs.GetInt("Year") < Int32.Parse(currentDate.Year.ToString()) && PlayerPrefs.GetInt("Month") < Int32.Parse(currentDate.Month.ToString()) && PlayerPrefs.GetInt("Day") < Int32.Parse(currentDate.Day.ToString()))
        {
            leftTime.SetActive(false);
        }
        else
        {
            leftTime.SetActive(true);
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
        NowSec = Int32.Parse(currentDate.Second.ToString());
        MinuteLeft = 60 - NowMinute;
        SecLeft = 60 - NowSec;

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
            if (SecLeft < 10)
            {
                leftTimeText.text = HourLeft + ":0" + MinuteLeft + ":0" + SecLeft;
            }
            else
            {
                leftTimeText.text = HourLeft + ":0" + MinuteLeft + ":" + SecLeft;
            }
        }
        else
        {
            if (SecLeft < 10)
            {
                leftTimeText.text = HourLeft + ":" + MinuteLeft + ":0" + SecLeft;
            }
            else
            {
                leftTimeText.text = HourLeft + ":" + MinuteLeft + ":" + SecLeft;
            }
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
            var currentDate = System.DateTime.Now;
            id = index.ToString();
            leftTime.SetActive(true);
            timeText[index].color = Color.black;
            if (NowHour < timeNum[index])
            {
                plus = true;
                PlayerPrefs.SetInt("PlusTimeBool", 1);
                PlayerPrefs.SetInt("Day", Int32.Parse(currentDate.Day.ToString())+1);
                PlayerPrefs.SetInt("Month", Int32.Parse(currentDate.Month.ToString()));
                PlayerPrefs.SetInt("Year", Int32.Parse(currentDate.Year.ToString()));
            }
            else
            {
                plus = false;
                PlayerPrefs.SetInt("PlusTimeBool", 0);
                PlayerPrefs.SetInt("Day", Int32.Parse(currentDate.Day.ToString()));
                PlayerPrefs.SetInt("Month", Int32.Parse(currentDate.Month.ToString()));
                PlayerPrefs.SetInt("Year", Int32.Parse(currentDate.Year.ToString()));
            }
        }


    }
}
