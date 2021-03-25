using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public void SkipButton()
    {
        SceneManager.LoadScene(3);
    }

    public void TicketsOpen()
    {
        SceneManager.LoadScene(4);
    }

    public void ChallengesOpen()
    {
        SceneManager.LoadScene(5);
    }

    public void PrizesOpen()
    {
        SceneManager.LoadScene(6);
    }

    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
