using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Email_panel : MonoBehaviour
{
    public GameObject shareToggle;
    public Text Friend;
    public GameObject FriendToggle;
    public Text Email;
    public GameObject EmailToggle;

    void Start()
    {
        
    }

    void Update()
    {
        if (Friend.text != "")
        {
            FriendToggle.SetActive(true);
        }

        if (Email.text != "")
        {
            EmailToggle.SetActive(true);
        }
    }

    public void Share()
    {
        shareToggle.SetActive(true);
    }
}
