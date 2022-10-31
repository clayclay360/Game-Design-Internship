using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmailSystem : MonoBehaviour
{
    public Email currentEmail;
    [Header("UI")]
    public GameObject emailPanel;
    public Text buttonText;
    [Header("Email Parts")]
    public Text subject;
    public Text sender;
    public Text body;

    private void Start()
    {
        ShowHide();
    }

    public void ChangeEmail(Email email)
    {
        subject.text = email.subject;
        sender.text = email.sender;
        body.text = email.body;
    }

    private void Update()
    {
        if (currentEmail)
        {
            ChangeEmail(currentEmail);
        }
    }

    public void ShowHide()
    {
        emailPanel.SetActive(!emailPanel.activeSelf); //switch the active state
        buttonText.text = emailPanel.activeSelf ? "Hide Email" : "Show Email";
    }
}
