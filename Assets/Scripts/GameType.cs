using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameType : MonoBehaviour
{
    void Start()
    {
        PlayerPrefs.SetInt("SnakeGameType", 1);
        PlayerPrefs.SetInt("CheckersType", 1);
        PlayerPrefs.SetInt("QuizGameType", 1);
    }

    public void Checkers_T()
    {
        PlayerPrefs.SetInt("CheckersType", 1);
    }

    public void Checkers_C()
    {
        PlayerPrefs.SetInt("CheckersType", 2);
    }

    public void Snake_T()
    {
        PlayerPrefs.SetInt("SnakeGameType", 1);
    }

    public void Snake_C()
    {
        PlayerPrefs.SetInt("SnakeGameType", 2);
    }

    public void Quiz_T()
    {
        PlayerPrefs.SetInt("QuizGameType", 1);
    }

    public void Quiz_C()
    {
        PlayerPrefs.SetInt("QuizGameType", 2);
    }
}
