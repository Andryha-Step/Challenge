﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private GameObject[] instPans;
    private Vector2[] panPos;
    private Vector2[] panScale;

    private RectTransform contentRect;
    private Vector2 contentVector;

    private int selectedPanID;
    private bool isScrolling;

    public void Start()
    {
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
    }

    private void FixedUpdate()
    {
        float nearestPos = float.MaxValue;
        for (int i = 0; i < panCount; i++)
        {
            float distance = Mathf.Abs(contentRect.anchoredPosition.x - panPos[i].x);
            if (distance < nearestPos)
            {
                nearestPos = distance;
                selectedPanID = i;
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
