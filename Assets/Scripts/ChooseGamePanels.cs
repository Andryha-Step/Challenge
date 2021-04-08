using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseGamePanels : MonoBehaviour
{
    public GameObject InfoPanel;
    public GameObject InfoBttn;

    public void Update()
    {
        if (GameObject.Find("Dropdown List") != null)
        {
            InfoPanel.SetActive(false);
        }
    }

    public void OpenInfoPanel()
    {
        InfoPanel.SetActive(true);
        InfoBttn.SetActive(false);
    }

    public void CloseInfoPanel()
    {
        InfoPanel.SetActive(false);
        InfoBttn.SetActive(true);
    }
}
