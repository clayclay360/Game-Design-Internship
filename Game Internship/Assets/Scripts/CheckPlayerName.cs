using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckPlayerName : MonoBehaviour
{
    private Text emailText;

    private void Start()
    {
        emailText = GetComponent<Text>();
    }

    public void CheckName()
    {
        emailText = GetComponent<Text>();
        string body = emailText.text;
        emailText.text = "";

        foreach (char c in body)
        {
            if (c == '*')
            {
                emailText.text += Player.Name;
            }
            else
            {
                emailText.text += c;
            }
        }
    }
}
