using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UserControl : MonoBehaviour
{
    public InputField Password;
    public string SavePassword;

    public void SaveButton()
    {
        SavePassword = (Password.text).ToString();

        if (SavePassword.Length == 6)
        {
            SavePassword = Password.ToString();
            PlayerPrefs.SetString("Password", (Password.text).ToString());
            PlayerPrefs.Save();
            SceneManager.LoadScene(2);
        }
    }
}
