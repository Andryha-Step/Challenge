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
            for (int i = 0; i<4; i++)
            {
                TourText[i].text = CheTourText[i];
            }
        }
        else
        {
            tournamentTime.SetActive(false);
            for (int i = 0; i < 4; i++)
            {
                TourText[i].text = OtherTourText[i];
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
