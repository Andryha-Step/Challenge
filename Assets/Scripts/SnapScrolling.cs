using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SnapScrolling : MonoBehaviour
{
    [Range(1, 50)]
    [Header("Controllers")] 
    public int panCount;
    [Range(1, 1000)]
    public int panOffset;
    [Range(0f, 20f)]
    public float snapSpeed;
    [Header("Other Objects")]
    public GameObject panPrefab;

    public GameObject[] instPans;
    public Vector2[] panPos;
    public Vector2[] panScale;

    public RectTransform contentRect;
    public Vector2 contentVector;

    public GameObject tournamentTime;

    public int selectedPanID;
    public bool isScrolling;

    public bool start;

    public Text[] TourText = new Text[4];
    public string[] CheTourText = new string[4];
    public string[] OtherTourText = new string[4];
    public Text startText;
    public Text info;

    int savePos;

    

    public void Start()
    {
        start = true;
        contentRect = GetComponent<RectTransform>();
        instPans = new GameObject[panCount];
        panPos = new Vector2[panCount];
        for (int i = 0; i < panCount; i++)
        {

            instPans[i] = Instantiate(panPrefab, transform, false);
            if (i == 0) continue;
            instPans[i].transform.localPosition = new Vector2(instPans[i - 1].transform.localPosition.x + panPrefab.GetComponent<RectTransform>().sizeDelta.x + panOffset, instPans[i].transform.localPosition.y);
            panPos[i] = -instPans[i].transform.localPosition;
        }
        if (selectedPanID == 0)
        {
            startText.text = CheTourText[0];
        }
        else
        {
            startText.text = OtherTourText[0];
        }

        savePos = selectedPanID;
    }

    private void FixedUpdate()
    {


        if (savePos != selectedPanID)
        {
            if (selectedPanID == 0)
            {
                startText.text = CheTourText[0];
            }
            else
            {
                startText.text = OtherTourText[0];
            }
            savePos = selectedPanID;
        }


        if (selectedPanID == 0)
        {
            tournamentTime.SetActive(true);
            for (int i = 0; i < 4; i++)
            {
                TourText[i].text = CheTourText[i];
            }

            if (startText.text == "Турнир на 4 человека")
            {
                info.text = "Стоимость билета: 6500₽\n\nКол - во призовых мест: 1\n\nПриз: Сертификаты на 20 000₽\n\n†";
            }
            else if (startText.text == "Турнир на 6 человек")
            {
                info.text = "Стоимость билета: 2166₽\n\nКол - во призовых мест: 2\n\nПриз: Сертификаты на 10 000₽\n\n†";
            }
            else if (startText.text == "Турнир на 8 человек")
            {
                info.text = "Стоимость билета: 650₽\n\nКол - во призовых мест: 3\n\nПриз: Сертификаты на 4000₽\n\n†";
            }
            else if (startText.text == "Турнир на 12 человек")
            {
                info.text = "Стоимость билета: 108₽\n\nКол - во призовых мест: 4\n\nПриз: Сертификаты на 1000₽\n\n†";
            }
        }
        else
        {
            tournamentTime.SetActive(false);
            for (int i = 0; i < 4; i++)
            {
                TourText[i].text = OtherTourText[i];
            }
            if (startText.text == "Турнир на 6 человек")
            {
                info.text = "Стоимость билета: 6500₽\n\nКол - во призовых мест: 2\n\nПриз: Сертификаты на 25 000₽\n\n†";
            }
            else if (startText.text == "Турнир на 10 человек")
            {
                info.text = "Стоимость билета: 1300₽\n\nКол - во призовых мест: 3\n\nПриз: Сертификаты на 8500₽\n\n†";
            }
            else if (startText.text == "Турнир на 16 человек")
            {
                info.text = "Стоимость билета: 325₽\n\nКол - во призовых мест: 3\n\nПриз: Сертификаты на 3500₽\n\n†";
            }
            else if (startText.text == "Турнир на 20 человек")
            {
                info.text = "Стоимость билета: 65₽\n\nКол - во призовых мест: 4\n\nПриз: Сертификаты на 700₽\n\n†";
            }
        }
        
        if (start == true)
        {
            if(PlayerPrefs.GetInt("PanID") == 1)
            {
                contentVector.x = -495;
                contentRect.anchoredPosition = contentVector;
            }
            else if (PlayerPrefs.GetInt("PanID") == 2)
            {
                contentVector.x = -990;
                contentRect.anchoredPosition = contentVector;
            }
            start = false;
        }

        float nearestPos = float.MaxValue;
        for (int i = 0; i < panCount; i++)
        {
            float distance = Mathf.Abs(contentRect.anchoredPosition.x - panPos[i].x);
            if (distance < nearestPos)
            {
                nearestPos = distance;
                selectedPanID = i;
                PlayerPrefs.SetInt("PanID", selectedPanID);
            }
        }
        if (isScrolling) return;
        contentVector.x = Mathf.SmoothStep(contentRect.anchoredPosition.x, panPos[selectedPanID].x, snapSpeed * Time.fixedDeltaTime);
        contentRect.anchoredPosition = contentVector;
    }

    public void Scrolling(bool scroll)
    {
        isScrolling = scroll;
    }

    public void Start_game()
    {
        if (selectedPanID == 0)
        {
            SceneManager.LoadScene(7);
        }
        else
        {
            if (selectedPanID == 1)
            {
                SceneManager.LoadScene(9);
            }
            else
            {
                SceneManager.LoadScene(13);
            }
        }
    }

}
